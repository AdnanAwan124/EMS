using EMS.Application.Common.Interfaces;
using EMS.Application.EmployeeModule.Commands.CreateEmployee;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.EmployeeModule.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DOB { get; set; }
        public int DepartmentId { get; set; }
    }

    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(t => t.Title).Length(1, 5);
            RuleFor(t => t.Name).Length(1, 100);
            RuleFor(t => t.Email).EmailAddress().Length(1, 100);
            RuleFor(t => t.DepartmentId).GreaterThan(0);
        }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IEMSDBContext _eMSDBContext;
        public UpdateEmployeeCommandHandler(IEMSDBContext eMSDBContext)
        {
            _eMSDBContext = eMSDBContext;
        }
        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _eMSDBContext.Employees.FirstOrDefaultAsync(t => t.Id == request.Id);

            if (employee == null)
            {
                return false;
            }

            employee.Title = request.Title;
            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.DOB = request.DOB;
            employee.DepartmentId = request.DepartmentId;

            await _eMSDBContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
