using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Common.Interfaces
{
    public interface IEMSDBContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Department> Departments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
