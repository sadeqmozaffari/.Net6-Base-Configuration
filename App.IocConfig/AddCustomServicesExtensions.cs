using App.Common;
using App.DataLayer.Contracts;
using App.DataLayer.UnitOfWork;
using App.Services;
using App.Services.Api;
using App.Services.Api.Contract;
using App.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;


namespace App.IocConfig
{
    public static class AddCustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient<IjwtService, jwtService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            return services;
        }
    }
}
