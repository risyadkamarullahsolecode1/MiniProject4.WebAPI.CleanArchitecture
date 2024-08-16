using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Application.Interfaces
{
    public interface IDepartmentServices
    {
        //method
        Task<IEnumerable<object>> GetDepartmentsWithMoreThan10Employees();
        Task<IEnumerable<object>> GetEmployeeDetailsByDepartment(string departmentName);
    }
}
