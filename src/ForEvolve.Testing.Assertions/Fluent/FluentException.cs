using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ForEvolve.Testing.Fluent
{
    public class FluentException : XunitException
    {
        public FluentException()
        {
        }

        public FluentException(string userMessage) : base(userMessage)
        {
        }

        protected FluentException(string userMessage, Exception innerException) : base(userMessage, innerException)
        {
        }

        protected FluentException(string userMessage, string stackTrace) : base(userMessage, stackTrace)
        {
        }
    }
}
