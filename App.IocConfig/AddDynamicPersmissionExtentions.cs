using App.Services;
using App.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.IocConfig
{
    public static class AddDynamicPersmissionExtentions
    {
        public static IServiceCollection AddDynamicPersmission(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
            services.AddSingleton<ISecurityTrimmingService, SecurityTrimmingService>();

            return services;
        }
    }
}
