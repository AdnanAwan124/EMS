using EMS.Application.Common.Interfaces;
using EMS.Application.EmployeeModule.Queries.GetEmployeeList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DepartmentModule.Queries.GetDepartmentList
{
    public class GetDepartmentListQuery : IRequest<List<GetDepartmentListQueryResponse>>
    {
    }

    public class GetDepartmentListQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, List<GetDepartmentListQueryResponse>>
    {
        private readonly IEMSDBContext _eMSDBContext;
        public GetDepartmentListQueryHandler(IEMSDBContext eMSDBContext)
        {
            _eMSDBContext = eMSDBContext;
        }
        public async Task<List<GetDepartmentListQueryResponse>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var departments = await _eMSDBContext.Departments
                .Select(t => new GetDepartmentListQueryResponse()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return departments;
        }
    }
}
