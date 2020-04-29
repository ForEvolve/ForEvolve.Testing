using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ForEvolve.Testing.AspNetCore.Identity
{
    public class UserManagerDependenciesTest
    {
        [Fact]
        public void Should_be_instantiable()
        {
            var helper = new UserManagerDependencies<FakeUser>();
        }
    }
}
