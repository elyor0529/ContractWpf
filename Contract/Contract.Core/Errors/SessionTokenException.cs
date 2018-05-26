using System;
using System.Runtime.Serialization;

namespace Contract.Core.Errors
{
    [Serializable]
    public class SessionTokenException : BaseException
    {
        public SessionTokenException()
        {
        }

        public SessionTokenException(string message) : base(message)
        {
        }

        public SessionTokenException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SessionTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}