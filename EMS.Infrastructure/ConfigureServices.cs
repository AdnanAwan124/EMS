using EMS.Application.Common.Interfaces;
using EMS.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EMS.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EMSDBContext>(o => o.UseInMemoryDatabase("EMSDb"));
            services.AddScoped<IEMSDBContext>(provider => provider.GetRequiredService<EMSDBContext>());

            services.SeedDepartmentsData();

            return services;
        }
    }
}
