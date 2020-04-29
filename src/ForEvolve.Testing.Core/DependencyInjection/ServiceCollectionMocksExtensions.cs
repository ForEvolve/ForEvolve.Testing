using Moq;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Represents Startup extensions that registers Mocks.
    /// </summary>
    public static class ServiceCollectionMocksExtensions
    {
        /// <summary>
        /// Register the specified type as a singleton mock.
        /// </summary>
        /// <typeparam name="TService">The type of the service to mock.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="setup">The setup to configure the mock.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddSingletonMock<TService>(this IServiceCollection services, Action<Mock<TService>> setup = null)
            where TService : class
        {
            var serviceMock = new Mock<TService>();
            setup?.Invoke(serviceMock);
            services.AddSingleton(serviceMock.Object);
            return services;
        }

        /// <summary>
        /// Register the specified type as a scoped dependency.
        /// </summary>
        /// <typeparam name="TService">The type of the service to mock.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="setup">The setup to configure the mock. The setup is executed once per scope.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddScopedMock<TService>(this IServiceCollection services, Action<Mock<TService>> setup = null)
            where TService : class
        {
            services.AddScoped(x =>
            {
                var serviceMock = new Mock<TService>();
                setup?.Invoke(serviceMock);
                return serviceMock.Object;
            });
            return services;
        }

        /// <summary>
        /// Register the specified type as a transient dependency.
        /// </summary>
        /// <typeparam name="TService">The type of the service to mock.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="setup">The setup to configure the mock. The setup is executed for every injected object of type TService.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddTransientMock<TService>(this IServiceCollection services, Action<Mock<TService>> setup = null)
            where TService : class
        {
            services.AddTransient(x =>
            {
                var serviceMock = new Mock<TService>();
                setup?.Invoke(serviceMock);
                return serviceMock.Object;
            });
            return services;
        }
    }
}
