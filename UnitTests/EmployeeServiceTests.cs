using Microsoft.Extensions.Logging;
using Moq;
using MutationWithStryker;

namespace UnitTests
{
    public class EmployeeServiceTests
    {
        [Theory]
        [InlineData(3300, 1, 0, 330)]
        [InlineData(3300, 1, 1, 215)]
        [InlineData(3300, 0, 1, 50)]
        public void CalculateBonus_WhenEmployeeIsValid(decimal salary, int childrenNumber, int extraShiftsNumber, decimal expectedBonus)
        {
            //Arrange
            var mockEmployee = new Employee
            {
                Name = "Jhon Doe",
                Address = "Around the world",
                MailAddress = "jhon.doe@email.com",
                Salaray = salary,
                Children = childrenNumber,
                ExtraShifts = extraShiftsNumber
            };

            var mockLogger = new Mock<ILogger<EmployeeService>>();

            var employeeService = new EmployeeService(mockLogger.Object);

            //Act
            var result = employeeService.CalculateBonus(mockEmployee);

            //Assert
            Assert.Equal(expectedBonus, result);
        }

        [Fact]
        public void CalculateBonus_WhenEmployeeIsNull()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<EmployeeService>>();

            var employeeService = new EmployeeService(mockLogger.Object);

            //Act
            var result = employeeService.CalculateBonus(null);

            //Assert
            Assert.Equal(0, result);
            mockLogger.VerifyLog(s => s.LogError(It.Is<string>(p => p.Contains("An expction was caught: ")), Times.AtLeastOnce()));
        }
    }
}