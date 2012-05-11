namespace GiveCRM.DataAccess
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using GiveCRM.Infrastructure;
    using Simple.Data;

    public class MultiTenantDatabaseProvider : IDatabaseProvider
    {
        private readonly ITenantCodeProvider tenantCodeProvider;
        private readonly IDictionary<string, string> connectionStrings = new Dictionary<string, string>();

        public MultiTenantDatabaseProvider(ITenantCodeProvider tenantCodeProvider)
        {
            this.tenantCodeProvider = tenantCodeProvider;
        }

        public dynamic GetDatabase()
        {
            string tenantCode = tenantCodeProvider.GetTenantCode();

            string connectionString;
            lock (connectionStrings)
            {
                if (!connectionStrings.TryGetValue(tenantCode, out connectionString))
                {
                    connectionString = GetConnectionString(tenantCode);
                    connectionStrings.Add(tenantCode, connectionString);
                }
            }

            return Database.OpenConnection(connectionString);
        }

        private string GetConnectionString(string tenantCode)
        {
            // TODO: Make this a service call to the Admin site rather than hitting the DB directly
            using (var transaction = TransactionScopeFactory.Create(true))
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GiveCRM_Admin"].ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 ConnectionString FROM Charity WHERE SubDomain = @subDomain";
                command.CommandType = CommandType.Text;
                var subDomainParameter = command.CreateParameter();
                subDomainParameter.ParameterName = "@subDomain";
                subDomainParameter.Value = tenantCode;

                var connectionString = (string) command.ExecuteScalar();
                
                transaction.Complete();

                return connectionString;
            }
        }
    }
}