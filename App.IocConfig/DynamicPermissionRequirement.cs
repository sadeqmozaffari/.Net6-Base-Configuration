using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace App.IocConfig
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DynamicPermissionsAuthorizationHandler(ISecurityTrimmingService securityTrimmingService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _securityTrimmingService = securityTrimmingService;
        }

        protected override Task HandleRequirementAsync(
             AuthorizationHandlerContext context,
             DynamicPermissionRequirement requirement)
        {
            var routeData = _httpContextAccessor.HttpContext.GetRouteData();
            
            if (routeData == null)
            {
                return Task.CompletedTask;
            }
            var areaName = routeData?.Values["area"]?.ToString();
            var area = string.IsNullOrWhiteSpace(areaName) ? string.Empty : areaName;

            var contollerName = routeData?.Values["controller"]?.ToString();
            var controller = string.IsNullOrWhiteSpace(contollerName) ? string.Empty : contollerName;

            var actionName = routeData?.Values["action"]?.ToString();
            var action = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;

            if (_securityTrimmingService.CanCurrentUserAccess(area, controller, action))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
