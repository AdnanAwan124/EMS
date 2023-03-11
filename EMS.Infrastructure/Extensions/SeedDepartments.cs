using EMS.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Extensions
{
    public static class SeedDepartments
    {
        public static void SeedDepartmentsData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<EMSDBContext>();

            if (!context.Departments.Any())
            {
                context.AddRange(new List<Department>()
                    {
                         new Department()
                    {
                        Id = 1,
                        Name = "Admin"
                    },
                    new Department()
                    {
                        Id = 2,
                        Name = "Custom"
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
