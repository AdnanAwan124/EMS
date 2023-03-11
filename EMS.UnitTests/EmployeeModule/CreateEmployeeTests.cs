using EMS.Application.Common.Interfaces;
using EMS.Application.EmployeeModule.Commands.CreateEmployee;
using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EMS.UnitTests.EmployeeModule
{
    [TestFixture]
    public class CreateEmployeeTests
    {

        [Test]
        public async Task CreateEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();
            var mockContext = new Mock<IEMSDBContext>();
            mockContext.Setup(m => m.Employees).Returns(mockSet.Object);

            var handler = new CreateEmployeeCommandHandler(mockContext.Object);
            var command = new CreateEmployeeCommand() { Email = "", Name = "test", Title = "" };

            var restul = await handler.Handle(command, CancellationToken.None);

            mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
