using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.SimpleLoad.Tests
{
    [Serializable]
    public class BadTimesException : Exception, ISerializable
    {
        public BadTimesException()
        {
        }

        public BadTimesException(string message) : base(message)
        {
        }

        public BadTimesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadTimesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
