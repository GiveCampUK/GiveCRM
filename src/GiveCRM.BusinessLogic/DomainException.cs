namespace GiveCRM.BusinessLogic
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception to be deliberatley thrown when exceptional circumstances arise in 
    /// the domain use-cases, as opposed to more "technical" or unexpected exceptions.
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DomainException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}