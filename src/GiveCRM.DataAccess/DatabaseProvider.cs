namespace GiveCRM.DataAccess
{
    using Simple.Data;

    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly dynamic database = Database.OpenNamedConnection("GiveCRM");

        public dynamic GetDatabase()
        {
            return database;
        }
    }
}