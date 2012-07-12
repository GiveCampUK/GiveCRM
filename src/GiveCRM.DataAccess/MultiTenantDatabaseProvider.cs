namespace GiveCRM.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using GiveCRM.Infrastructure;
    using Simple.Data;

    public class MultiTenantDatabaseProvider : IDatabaseProvider
    {
        private readonly ITenantCodeProvider tenantCodeProvider;
        private readonly IDictionary<string, ConnectionDetails> cache = new Dictionary<string, ConnectionDetails>();

        public MultiTenantDatabaseProvider(ITenantCodeProvider tenantCodeProvider)
        {
            this.tenantCodeProvider = tenantCodeProvider;
        }

        public dynamic GetDatabase()
        {
            string tenantCode = tenantCodeProvider.GetTenantCode();

            ConnectionDetails connectionDetails;
            lock (this.cache)
            {
                if (!this.cache.TryGetValue(tenantCode, out connectionDetails))
                {
                    connectionDetails = GetConnectionString(tenantCode);

                    if (connectionDetails == null)
                    {
                        throw new InvalidOperationException(string.Format("Unable to obtain connection details for tenant code {0}", tenantCode));
                    }

                    this.cache.Add(tenantCode, connectionDetails);
                }
            }

            return Database.OpenConnection(connectionDetails.ConnectionString)[connectionDetails.DatabaseSchema];
        }

        private ConnectionDetails GetConnectionString(string tenantCode)
        {
            ConnectionDetails result = null;
            using (var transaction = TransactionScopeFactory.Create(true))
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GiveCRMAdmin"].ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 ConnectionString, DatabaseSchema FROM Charity WHERE TenantCode = @tenantCode";
                var tenantCodeParam = command.CreateParameter();
                tenantCodeParam.ParameterName = "@tenantCode";
                tenantCodeParam.SqlDbType = SqlDbType.NVarChar;
                tenantCodeParam.Value = tenantCode;
                command.Parameters.Add(tenantCodeParam);
                
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new ConnectionDetails
                                     {
                                         ConnectionString = reader.GetString(0),
                                         DatabaseSchema = reader.GetString(1)
                                     };
                    }
                }

                transaction.Complete();
            }

            return result;
        }

        private ConnectionDetails GetConnectionString2(string tenantCode)
        {
            // TODO: Make this a service call to the Admin site rather than hitting the DB directly
            using (var transaction = TransactionScopeFactory.Create(true))
            {
                var database = Database.OpenConnection(ConfigurationManager.ConnectionStrings["GiveCRMAdmin"].ConnectionString);

                var charity = database.Charity.FindByTenantCode(tenantCode); //.Select(database.Charity.ConnectionString, database.Charity.DatabaseSchema);
                
                transaction.Complete();

                if (charity == null)
                {
                    throw new InvalidOperationException(string.Format("Not able to find charity for tenant code {0}", tenantCode));
                }

                return new ConnectionDetails { ConnectionString = charity.ConnectionString, DatabaseSchema = charity.DatabaseSchema };
            }
        }
    }

    public class ConnectionDetails
    {
        public string ConnectionString { get; set; }
        public string DatabaseSchema { get; set; }
    }
}