using Microsoft.Extensions.Logging;

namespace MutationWithStryker
{
    public class EmployeeService
    {
        ILogger _logger;

        public EmployeeService(ILogger<EmployeeService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Calculate the bonus for a given employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public decimal CalculateBonus(Employee employee)
        {
            try
            {
                if (employee == null || string.IsNullOrEmpty(employee.Name) || employee.Salaray == 0)
                {
                    throw new InvalidOperationException("Invalid Employee");
                }

                if (employee.ExtraShifts > 0)
                    return employee.Salaray / 20 * employee.Children + employee.ExtraShifts * 50;
                else if (employee.Children > 0)
                    return employee.Salaray / 10 * employee.Children;

                return 100;
            }
            catch (Exception ex)
            {
                _logger.LogError("An expction was caught: " + ex.Message);
                return 0;
            }
        }
    }
}
