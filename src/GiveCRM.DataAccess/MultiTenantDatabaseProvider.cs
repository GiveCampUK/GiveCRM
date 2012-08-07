namespace GiveCRM.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
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
                        var message = new StringBuilder();
                        message.AppendFormat("Unable to obtain connection details for tenant code {0}.", tenantCode).AppendLine();
                        message.AppendFormat(
                            "Because we use the domain name (in this case \"{0}\") from the URL to look up the Charity for multitenancy, this means there must be no record in the Admin database Charity table with a TenantCode of \"{0}\".",
                            tenantCode).AppendLine();
                        message.AppendLine("If this is the case and you wish to add the record, here's one we prepared earlier:");
                        message.AppendFormat("-- INSERT INTO Charity (Name, RegisteredCharityNumber, TenantCode, ConnectionString, DatabaseSchema) VALUES (Name, RegisteredCharityNumber, '{0}', ConnectionString, DatabaseSchema)", tenantCode);

                        throw new TenantNotFoundException(message.ToString());
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
    }

    public class ConnectionDetails
    {
        public string ConnectionString { get; set; }
        public string DatabaseSchema { get; set; }
    }
}