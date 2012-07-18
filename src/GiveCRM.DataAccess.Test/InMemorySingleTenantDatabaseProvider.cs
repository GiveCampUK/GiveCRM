namespace GiveCRM.DataAccess.Test
{
    using Simple.Data;

    public class InMemorySingleTenantDatabaseProvider : IDatabaseProvider
    {
        private readonly dynamic db;

        public InMemoryAdapter Adapter { get; private set; }

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
    }
}