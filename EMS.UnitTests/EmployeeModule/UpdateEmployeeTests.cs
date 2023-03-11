using EMS.Application.Common.Interfaces;
using EMS.Application.EmployeeModule.Commands.CreateEmployee;
using EMS.Application.EmployeeModule.Commands.UpdateEmployee;
using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace EMS.UnitTests.EmployeeModule
{
    [TestFixture]
    public class UpdateEmployeeTests
    {
        [Test]
        public async Task UpdateEmployee_NotFound()
        {
            //Arrange
            var employeeList = new List<Employee>()
            {
                new Employee() { Id = 1,}
            };

            var mockContext = new Mock<IEMSDBContext>();
            mockContext.Setup(x => x.Employees)
                            .ReturnsDbSet(employeeList);

            var handler = new UpdateEmployeeCommandHandler(mockContext.Object);
            var command = new UpdateEmployeeCommand() { Id = 2, Email = "", Name = "", Title = "" };

            //Act
            var restul = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(restul);
        }

        [Test]
        public async Task UpdateEmployee_Success()
        {
            //Arrange
            var mockContext = new Mock<IEMSDBContext>();
            mockContext.Setup(x => x.Employees)
                            .ReturnsDbSet(GetFakeEmployeeList());

            var handler = new UpdateEmployeeCommandHandler(mockContext.Object);
            var command = new UpdateEmployeeCommand() { Id = 1, Email = "", Name = "", Title = "" };

            //Act
            var restul = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(restul);
        }

        private static List<Employee> GetFakeEmployeeList()
        {
            return new List<Employee>()
            {
                new Employee() { Id = 1,}
            };
        }
    }
}
