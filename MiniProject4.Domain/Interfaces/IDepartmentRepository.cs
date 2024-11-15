using MiniProject4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> AddDepartment(Department department);
        Task<Department> UpdateDepartment(int id, Department department);
        Task DeleteDepartment(int id);
        Task<IEnumerable<Employee>> GetEmployee(int deptNo);
        Task<Department> GetDepartmentByName(string name);
    }
}
