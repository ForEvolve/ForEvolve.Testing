using ForEvolve.Testing.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System
{
    public static class ShouldExtensions
    {
        public static ObjectAssertion Should(this object @object)
        {
            return new ObjectAssertion(@object);
        }
    }
}