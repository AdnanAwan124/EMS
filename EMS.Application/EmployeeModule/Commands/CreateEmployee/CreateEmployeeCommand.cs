using EMS.Application.Common.Interfaces;
using EMS.Domain.Entities;
using EMS.Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.EmployeeModule.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string Title { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DOB { get; set; }
        public int DepartmentId { get; set; }
    }

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(t => t.Title).Length(1, 5);
            RuleFor(t => t.Name).Length(1, 100);
            RuleFor(t => t.Email).EmailAddress().Length(1, 100);
            RuleFor(t => t.DepartmentId).GreaterThan(0);
        }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEMSDBContext _eMSDBContext;
        public CreateEmployeeCommandHandler(IEMSDBContext eMSDBContext)
        {
            _eMSDBContext = eMSDBContext;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                Title = request.Title,
                Name = request.Name,
                Email = request.Email,
                DOB = request.DOB,
                DepartmentId = request.DepartmentId,
            };
            _eMSDBContext.Employees.Add(employee);
            await _eMSDBContext.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}
