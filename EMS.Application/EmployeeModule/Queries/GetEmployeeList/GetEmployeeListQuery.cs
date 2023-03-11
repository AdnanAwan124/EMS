using EMS.Application.Common.Interfaces;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.EmployeeModule.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<List<GetEmployeeListQueryResponse>>
    {
        public string? SearchKeyword { get; set; }
    }

    public class GetEmployeeListQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DOB { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, List<GetEmployeeListQueryResponse>>
    {
        private readonly IEMSDBContext _eMSDBContext;
        public GetEmployeeListQueryHandler(IEMSDBContext eMSDBContext)
        {
            _eMSDBContext = eMSDBContext;
        }
        public async Task<List<GetEmployeeListQueryResponse>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            //we can use some mapper here to do the mapping like automapper
            var employees = await _eMSDBContext.Employees.Include(t => t.Department)
                .Select(t => new GetEmployeeListQueryResponse()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Name = t.Name,
                    Email = t.Email,
                    DOB = t.DOB,
                    DepartmentId = t.DepartmentId,
                    DepartmentName = t.Department.Name,
                })
                .Where(t => string.IsNullOrEmpty(request.SearchKeyword) ||
                t.Title.Contains(request.SearchKeyword, StringComparison.InvariantCultureIgnoreCase) ||
                t.Name.Contains(request.SearchKeyword, StringComparison.InvariantCultureIgnoreCase) ||
                t.Email.Contains(request.SearchKeyword, StringComparison.InvariantCultureIgnoreCase) ||
                t.DepartmentName.Contains(request.SearchKeyword, StringComparison.InvariantCultureIgnoreCase)
                ).ToListAsync();

            return employees;
        }
    }
}
