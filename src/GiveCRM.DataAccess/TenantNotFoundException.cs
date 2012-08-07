namespace GiveCRM.DataAccess
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class TenantNotFoundException : Exception
    {
        public TenantNotFoundException()
        {
        }

        public TenantNotFoundException(string message) : base(message)
        {
        }

        public TenantNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TenantNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}