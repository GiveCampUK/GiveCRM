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
            // TODO: Make this a service call to the Admin site rather than hitting the DB directly
            using (var transaction = TransactionScopeFactory.Create(true))
            {
                var database = Database.OpenNamedConnection("GiveCRMAdmin");

                var charity = database.Charity.FindByTenantCode(tenantCode);

                transaction.Complete();

                if (charity == null)
                {
                    throw new TenantNotFoundException(string.Format("Not able to find charity for tenant code {0}", tenantCode));
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