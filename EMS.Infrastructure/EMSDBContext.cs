using EMS.Application.Common.Interfaces;
using EMS.Domain.Entities;
using EMS.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure
{
    public class EMSDBContext : DbContext, IEMSDBContext
    {
        public EMSDBContext(DbContextOptions<EMSDBContext> context) : base(context)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
