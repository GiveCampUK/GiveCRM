namespace GiveCRM.DataAccess
{
    using Simple.Data;

    public class SingleTenantDatabaseProvider : IDatabaseProvider
    {
        private readonly dynamic database = Database.OpenNamedConnection("GiveCRM");

        public dynamic GetDatabase()
        {
            return database;
        }
    }
}