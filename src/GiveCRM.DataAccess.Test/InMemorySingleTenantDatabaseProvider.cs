namespace GiveCRM.DataAccess.Test
{
    using System;
    using Simple.Data;

    public class InMemorySingleTenantDatabaseProvider : IDatabaseProvider
    {
        private dynamic db;

        private InMemoryAdapter Adapter { get; set; }
        
        public dynamic GetDatabase()
        {
            if (Adapter == null)
            {
                throw new InvalidOperationException("Adapter is not set. Please ensure you call Configure() before using the adapter.");
            }

            return this.db;
        }

        public IDatabaseProvider Configure(Action<InMemoryAdapter> configurer)
        {
            Adapter = new InMemoryAdapter();
            Database.UseMockAdapter(Adapter);
            this.db = Database.Open();
            configurer(Adapter);
            return this;
        }
    }
}