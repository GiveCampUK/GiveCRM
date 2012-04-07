namespace GiveCRM.DataAccess.Test
{
    public class FakeDatabaseProvider : IDatabaseProvider
    {
        private readonly dynamic database;

        public FakeDatabaseProvider(dynamic database)
        {
            this.database = database;
        }

        public dynamic GetDatabase()
        {
            return this.database;
        }
    }
}