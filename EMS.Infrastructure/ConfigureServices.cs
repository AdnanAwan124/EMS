using EMS.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EMSDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EMSDbConnection"));
            });
            services.AddScoped<IEMSDBContext>(provider => provider.GetRequiredService<EMSDBContext>());

            return services;
        }
    }
}
