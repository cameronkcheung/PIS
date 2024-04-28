using PIS.Common.Models;
using PIS.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PIS.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTenants(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantContextAccessor, TenantContextAccessor>();

            services.Configure<List<Tenant>>(configuration.GetSection("Tenants"));

            return services;
        }
    }
}
