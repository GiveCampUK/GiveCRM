namespace GiveCRM.BusinessLogic
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidBusinessOperationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidBusinessOperationException()
        {
        }

        public InvalidBusinessOperationException(string message) : base(message)
        {
        }

        public InvalidBusinessOperationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidBusinessOperationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}