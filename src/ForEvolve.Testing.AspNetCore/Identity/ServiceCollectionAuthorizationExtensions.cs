using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionAuthorizationExtensions
    { 
        public static IServiceCollection ByPassPolicies(this IServiceCollection services, IEnumerable<string> policiesName)
        {
            var authorizedPolicy = CreateAuthorizedPolicy();
            return services.PostConfigure<AuthorizationOptions>(options =>
            {
                foreach (var policyName in policiesName)
                {
                    options.AddPolicy(policyName, authorizedPolicy);
                }
                options.DefaultPolicy = authorizedPolicy;
            });
        }

        public static IServiceCollection ByPassDefaultPolicy(this IServiceCollection services)
        {
            var authorizedPolicy = CreateAuthorizedPolicy();
            return services.PostConfigure<AuthorizationOptions>(options =>
            {
                options.DefaultPolicy = authorizedPolicy;
            });
        }

        public static IServiceCollection ReplacePolicy(this IServiceCollection services, string policyName, AuthorizationPolicy policy)
        {
            return services.PostConfigure<AuthorizationOptions>(options =>
            {
                options.AddPolicy(policyName, policy);
            });
        }

        private static AuthorizationPolicy CreateAuthorizedPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAssertion(x => true)
                .Build();
        }
    }

}
