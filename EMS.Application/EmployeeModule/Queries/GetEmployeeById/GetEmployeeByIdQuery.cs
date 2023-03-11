using EMS.Application.Common.Interfaces;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.EmployeeModule.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeByIdQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DOB { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdQueryResponse>
    {
        private readonly IEMSDBContext _eMSDBContext;
        public GetEmployeeByIdQueryHandler(IEMSDBContext eMSDBContext)
        {
            _eMSDBContext = eMSDBContext;
        }
        public async Task<GetEmployeeByIdQueryResponse?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _eMSDBContext.Employees.Include(t => t.Department)
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (employee == null)
            {
                return null;
            }

            return new GetEmployeeByIdQueryResponse
            {
                Id = employee.Id,
                Title = employee.Title,
                Name = employee.Name,
                Email = employee.Email,
                DOB = employee.DOB,
                DepartmentId = employee.Department.Id,
                DepartmentName = employee.Department.Name,
            };
        }
    }
}
