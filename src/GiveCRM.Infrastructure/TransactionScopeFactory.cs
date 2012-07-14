namespace GiveCRM.Infrastructure
{
    using System.Transactions;

    public static class TransactionScopeFactory
    {
        public static TransactionScope Create(bool requiresNew)
        {
            var option = requiresNew ? TransactionScopeOption.RequiresNew : TransactionScopeOption.Required;
            return new TransactionScope(option, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
        }

        public static TransactionScope Create()
        {
            return Create(false);
        }
    }
}