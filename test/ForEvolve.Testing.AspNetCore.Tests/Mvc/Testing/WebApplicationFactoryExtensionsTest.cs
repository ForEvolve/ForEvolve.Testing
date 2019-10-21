using AspNetCoreTestApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP_3
using Microsoft.Extensions.Hosting;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Testing.AspNetCore.Mvc.Testing
{
    public class WebApplicationFactoryExtensionsTest : IClassFixture<MyAppFactory>
    {
        private readonly MyAppFactory _factory;
        public WebApplicationFactoryExtensionsTest(MyAppFactory factory)
        {
            if (factory == null) { throw new ArgumentNullException(nameof(factory)); }
            _factory = factory;
        }

        public class FindServiceCollection : WebApplicationFactoryExtensionsTest
        {
            public FindServiceCollection(MyAppFactory factory)
                : base(factory) { }

            [Fact]
            public void Should_return_the_IServiceCollection()
            {
                // Arrange
                var client = _factory.CreateClient();

                // Act
                var services = _factory.FindServiceCollection();

                // Assert
                Assert.NotNull(services);
            }
        }
    }

    public class MyAppFactory : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
        }
    }
}
