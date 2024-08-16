using Microsoft.Extensions.Configuration;
using MiniProject4.Application.Interfaces;
using MiniProject4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Application.Services
{
    public class DepartmentService:IDepartmentServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorksonRepository _worksonRepository;
        private readonly IConfiguration _configuration;
        public DepartmentService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IProjectRepository projectRepository, IWorksonRepository worksonRepository, IConfiguration configuration)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _worksonRepository = worksonRepository;
            _configuration = configuration;
        }
        public async Task<IEnumerable<object>> GetDepartmentsWithMoreThan10Employees()
        {
            var departments = await _departmentRepository.GetAllDepartments();

            return departments
                .GroupBy(e => e.Deptno)
                .Where(g => g.Count() > 10)
                .Select(g => new { Deptno = g.Key, EmployeeCount = g.Count() })
                .ToList();
        }
        public async Task<IEnumerable<object>> GetEmployeeDetailsByDepartment(string departmentName)
        {
            var departments = await _departmentRepository.GetAllDepartments();
            var employees = await _employeeRepository.GetAllEmployees();

            return employees
                .Join(departments, e => e.Deptno, d => d.Deptno, (e, d) => new { e, d })
                .Where(x => x.d.Deptname == departmentName)
                .Select(x => new
                {
                    x.e.Fname,
                    x.e.Lname,
                    x.e.Address
                })
                .ToList();
        }
    }
}
