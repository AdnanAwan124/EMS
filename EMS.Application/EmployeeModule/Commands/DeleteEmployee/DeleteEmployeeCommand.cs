using EMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.EmployeeModule.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEMSDBContext _eMSDBContext;
        public DeleteEmployeeCommandHandler(IEMSDBContext eMSDBContext)
        {
            _eMSDBContext = eMSDBContext;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _eMSDBContext.Employees.FirstOrDefaultAsync(t => t.Id == request.Id);

            if (employee == null)
            {
                return false;
            }

            //currently hard removed the employee, in real scenerio we can also soft delete.
            _eMSDBContext.Employees.Remove(employee);
            await _eMSDBContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
