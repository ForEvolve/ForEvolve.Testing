﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionMvcExtensions
    {
        public static IServiceCollection RemoveFilter<TFilter>(this IServiceCollection services)
            where TFilter : IFilterMetadata
        {
            return services.PostConfigure<MvcOptions>(options =>
            {
                var filters = options.Filters
                    .Where(x => x is TypeFilterAttribute)
                    .Select(x => x as TypeFilterAttribute)
                    .Where(x => x.ImplementationType == typeof(TFilter))
                    .AsEnumerable<IFilterMetadata>()
                    .Union(options.Filters
                        .Where(x => x is ServiceFilterAttribute)
                        .Select(x => x as ServiceFilterAttribute)
                        .Where(x => x.ServiceType == typeof(TFilter))
                    )
                    .Union(options.Filters
                        .Where(x => x.GetType() == typeof(TFilter))
                    )
                    .ToArray()
                    ;
                if (filters != null && filters.Count() > 0)
                {
                    foreach (var filter in filters)
                    {
                        options.Filters.Remove(filter);
                    }
                }
            });
        }
    }

}
