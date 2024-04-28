using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PIS.Infrastructure.Constants;
using PIS.Application.Data;
using PIS.Infrastructure.Data;

namespace PIS.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration[ConfigurationConstants.ConnectionStringsProductInformation];
            services.AddDbContext<ProductInformationDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IProductInformationDbContext, ProductInformationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
