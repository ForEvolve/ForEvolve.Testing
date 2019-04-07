using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionAssertExtensions
    {
        public static IServiceCollection AssertAllServicesAreRegistered(this IServiceCollection services,
            IEnumerable<Type> expectedSingletonServices = null,
            IEnumerable<Type> expectedScopedServices = null,
            IEnumerable<Type> expectedTransientServices = null
        )
        {
            var hasSingletonServices = expectedSingletonServices != null;
            var hasScopedServices = expectedScopedServices != null;
            var hasTransientServices = expectedTransientServices != null;

            // Validate empty collections
            var hasNoService = !hasSingletonServices && !hasScopedServices && !hasTransientServices;
            if (hasNoService)
            {
                throw new EmptyException(services);
            }

            // Validate services by scope
            if (hasSingletonServices)
            {
                services.AssertSingletonServicesExist(expectedSingletonServices);
            }
            if (hasScopedServices)
            {
                services.AssertScopedServicesExist(expectedScopedServices);
            }
            if (hasTransientServices)
            {
                services.AssertTransientServicesExist(expectedTransientServices);
            }
            return services;
        }

        public static IServiceCollection AssertSingletonServicesExist(this IServiceCollection services, IEnumerable<Type> expectedServices)
        {
            return services.AssertServicesExistInScope(
                expectedServices,
                ServiceLifetime.Singleton
            );
        }

        public static IServiceCollection AssertScopedServicesExist(this IServiceCollection services, IEnumerable<Type> expectedServices)
        {
            return services.AssertServicesExistInScope(
                expectedServices,
                ServiceLifetime.Scoped
            );
        }

        public static IServiceCollection AssertTransientServicesExist(this IServiceCollection services, IEnumerable<Type> expectedServices)
        {
            return services.AssertServicesExistInScope(
                expectedServices,
                ServiceLifetime.Transient
            );
        }

        public static IServiceCollection AssertServicesExistInScope(this IServiceCollection services,
            IEnumerable<Type> expectedServices,
            ServiceLifetime lifetime
        )
        {
            var registeredServiceType = services
                .Where(x => x.Lifetime == lifetime)
                .Select(x => x.ServiceType);

            var missingServices = expectedServices
                .Except(registeredServiceType)
                .Select(x => x.Name);
            var unexpectedServices = registeredServiceType
                .Except(expectedServices)
                .Select(x => x.Name);

            var missingServicesCount = missingServices.Count();
            var unexpectedServicesCount = unexpectedServices.Count();

            if (missingServicesCount > 0 || unexpectedServicesCount > 0)
            {
                var message = "Invalid services.";
                if (missingServicesCount > 0)
                {
                    var missingServicesName = string.Join(", ", missingServices);
                    message += $"\r\nThe following {missingServicesCount} service(s) are missing: {missingServicesName}";
                }
                if (unexpectedServicesCount > 0)
                {
                    var unexpectedServicesName = string.Join(", ", unexpectedServices);
                    message += $"\r\nThe following {unexpectedServicesCount} service(s) were not expected: {unexpectedServicesName}";
                }
                throw new XunitException(message);
            }
            return services;
        }

        #region AssertSingletonServiceExists

        public static IServiceCollection AssertSingletonServiceExists<TService>(this IServiceCollection services)
        {
            return services.AssertServiceExistsInScope<TService>(ServiceLifetime.Singleton);
        }

        public static IServiceCollection AssertSingletonServiceExists(this IServiceCollection services, Type serviceType)
        {
            return services.AssertServiceExistsInScope(ServiceLifetime.Singleton, serviceType);
        }

        public static IServiceCollection AssertSingletonServiceExists<TService, TImplementation>(this IServiceCollection services)
        {
            return services.AssertServiceExistsInScope<TService, TImplementation>(ServiceLifetime.Singleton);
        }

        public static IServiceCollection AssertSingletonServiceExists(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            return services.AssertServiceExistsInScope(ServiceLifetime.Singleton, serviceType, implementationType);
        }

        [Obsolete("Use AssertSingletonServiceExists instead.")]
        public static IServiceCollection AssertSingletonServiceImplementationExists<TService, TImplementation>(this IServiceCollection services)
        {
            return services.AssertSingletonServiceExists<TService, TImplementation>();
        }

        #endregion

        #region AssertScopedServiceExists

        public static IServiceCollection AssertScopedServiceExists<TService>(this IServiceCollection services)
        {
            return services.AssertServiceExistsInScope<TService>(ServiceLifetime.Scoped);
        }

        public static IServiceCollection AssertScopedServiceExists<TService, TImplementation>(this IServiceCollection services)
        {
            return services.AssertServiceExistsInScope<TService, TImplementation>(ServiceLifetime.Scoped);
        }

        public static IServiceCollection AssertScopedServiceExists(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            return services.AssertServiceExistsInScope(ServiceLifetime.Scoped, serviceType, implementationType);
        }

        [Obsolete("Use AssertScopedServiceExists instead.")]
        public static IServiceCollection AssertScopedServiceImplementationExists<TService, TImplementation>(this IServiceCollection services)
        {
            return services.AssertScopedServiceExists<TService, TImplementation>();
        }

        #endregion

        #region AssertTransientServiceExists

        public static IServiceCollection AssertTransientServiceExists<TService>(this IServiceCollection services)
        {
            return services.AssertServiceExistsInScope<TService>(ServiceLifetime.Transient);
        }

        public static IServiceCollection AssertTransientServiceExists<TService, TImplementation>(this IServiceCollection services)
        {
            return services.AssertServiceExistsInScope<TService, TImplementation>(ServiceLifetime.Transient);
        }

        public static IServiceCollection AssertTransientServiceExists<TService, TImplementation>(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            return services.AssertServiceExistsInScope(ServiceLifetime.Transient, serviceType, implementationType);
        }

        [Obsolete("Use AssertTransientServiceExists instead.")]
        public static IServiceCollection AssertTransientServiceImplementationExists<TService, TImplementation>(this IServiceCollection services)
        {
            return services.AssertTransientServiceExists<TService, TImplementation>();
        }

        #endregion

        #region AssertServiceExistsInScope

        public static IServiceCollection AssertServiceExistsInScope<TService>(
            this IServiceCollection services, ServiceLifetime lifetime)
        {
            return services.AssertServiceExistsInScope(lifetime, typeof(TService));
        }

        public static IServiceCollection AssertServiceExistsInScope(
            this IServiceCollection services,
            ServiceLifetime lifetime, Type serviceType)
        {
            var result = services
                .Where(x => x.Lifetime == lifetime && x.ServiceType == serviceType)
                .Any();
            if (!result)
            {
                throw new TrueException($"No service of type {serviceType} was found with a lifetime of {lifetime}.", result);
            }
            return services;
        }

        public static IServiceCollection AssertServiceExistsInScope<TService, TImplementation>(
            this IServiceCollection services, ServiceLifetime lifetime)
        {
            return services.AssertServiceExistsInScope(
                lifetime,
                typeof(TService),
                typeof(TImplementation)
            );
        }

        [Obsolete("Use AssertServiceExistsInScope instead.")]
        public static IServiceCollection AssertServiceImplementationExistsInScope<TService, TImplementation>(
            this IServiceCollection services, ServiceLifetime lifetime)
        {
            return services.AssertServiceExistsInScope<TService, TImplementation>(lifetime);
        }

        public static IServiceCollection AssertServiceExistsInScope(
            this IServiceCollection services, 
            ServiceLifetime lifetime, Type serviceType, Type implementationType)
        {
            // Try to find AddX<TService, TImplementation>()
            var result = services
                .Where(
                    x => x.Lifetime == lifetime &&
                    x.ServiceType == serviceType &&
                    x.ImplementationType == implementationType
                )
                .Any();
            if (!result)
            {
                // Try to find AddX<TService>(s => Some factory/provider)
                var factoryResult = services
                    .Where(
                        x => x.Lifetime == lifetime &&
                        x.ServiceType == serviceType &&
                        x.ImplementationFactory != null
                    )
                    .Any();
                if (factoryResult)
                {
                    // When an ImplementationFactory exists, get the 
                    // service implementation and make sure it is of 
                    // the expected type.
                    var service = services.BuildServiceProvider().GetService(serviceType);
                    if (service.GetType() == implementationType)
                    {
                        result = true;
                    }
                }
            }
            if (!result)
            {
                throw new TrueException($"No implementation of type {implementationType} was found for service type {serviceType} with a lifetime of {lifetime}.", result);
            }
            return services;
        }

        #endregion
    }
}
