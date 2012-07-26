namespace GiveCRM.DataAccess.Test
{
    using System;
    using Simple.Data;

    public class InMemorySingleTenantDatabaseProvider : IDatabaseProvider
    {
        private readonly dynamic db;

        private InMemoryAdapter Adapter { get; set; }

        public InMemorySingleTenantDatabaseProvider()
        {
            Adapter = new InMemoryAdapter();
            Database.UseMockAdapter(Adapter);
            this.db = Database.Open();
        }

        public dynamic GetDatabase()
        {
            return this.db;
        }

        public IDatabaseProvider Configure(Action<InMemoryAdapter> configurer)
        {
            configurer(Adapter);
            return this;
        }
    }
}