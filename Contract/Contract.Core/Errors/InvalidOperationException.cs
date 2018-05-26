using System;
using System.Runtime.Serialization;

namespace Contract.Core.Errors
{
    [Serializable]
    public class InvalidOperationException : BaseException
    {
        public InvalidOperationException()
        {
        }

        public InvalidOperationException(string message) : base(message)
        {
        }

        public InvalidOperationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}