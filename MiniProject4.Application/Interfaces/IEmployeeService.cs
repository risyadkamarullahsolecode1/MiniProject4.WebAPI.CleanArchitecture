using MiniProject4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> ValidateRetirementAsync(Employee employee);
        Task<bool> CanAddToITDepartmentAsync();
        // method
        Task<IEnumerable<Employee>> GetEmployeesBrics();
        Task<IEnumerable<Employee>> GetEmployeeBornBetween1980And1990();
        Task<IEnumerable<Employee>> GetFemaleEmployeeBornAfter1990();
        Task<IEnumerable<Employee>> GetFemaleManagers();
        Task<IEnumerable<Employee>> GetNonManagerEmployees();
        Task<IEnumerable<Employee>> GetITDepartmentEmployeesAsync();
    }
}
