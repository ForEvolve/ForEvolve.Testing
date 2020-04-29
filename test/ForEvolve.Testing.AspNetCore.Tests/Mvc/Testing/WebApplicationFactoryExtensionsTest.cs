using AspNetCoreTestApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP_3 || NET5
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

            [Fact]
            public void Should_return_the_IServiceCollection_that_contains_registered_services()
            {
                // Arrange
                var factory = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(s =>
                    {
                        s.AddSingleton<IService, MyService>();
                    });
                });
                var client = factory.CreateClient();

                // Act
                var services = factory.FindServiceCollection();

                // Assert
                Assert.NotNull(services);
                services.AssertSingletonServiceExists<IService, MyService>();
            }

            private interface IService { }
            private class MyService : IService { }
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
