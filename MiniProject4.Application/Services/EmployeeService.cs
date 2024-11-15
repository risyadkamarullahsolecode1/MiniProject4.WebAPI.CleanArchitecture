﻿using Microsoft.Extensions.Configuration;
using MiniProject4.Application.Interfaces;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Application.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorksonRepository _worksonRepository;
        private readonly IConfiguration _configuration;
        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IProjectRepository projectRepository, IWorksonRepository worksonRepository, IConfiguration configuration)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _worksonRepository = worksonRepository;
            _configuration = configuration;
        }

        //penggunaan constraint untuk pengecekan tanggal dob pada employee yang harus retire
        public async Task<bool> ValidateRetirementAsync(Employee employee)
        {
            var retirementAge = _configuration.GetValue<int>("CompanySettings:RetireEmployee");
            var today = DateTime.Now.Year;

            var employeeAge = today - employee.Dob.Value.Year;
            return employeeAge >= retirementAge;
        }

        //penggunaan constraint MaxEmployeeITDepartemnt 
        public async Task<bool> CanAddToITDepartmentAsync()
        {
            var maxITEmployees = _configuration.GetValue<int>("Constraints:ITDepartmentMaxEmployee"); 
            
            var currentITEmployeeCount = (await GetITDepartmentEmployeesAsync()).Count();
            
            return currentITEmployeeCount >= maxITEmployees;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesBrics()
        {
            var bricsCountries = new List<string> { "Brazil", "Russia", "India", "China", "South Africa" };
            var employees = await _employeeRepository.GetAllEmployees();

            return employees
                .Where(e => bricsCountries.Any(address => e.Address.Contains(address)))
                .OrderBy(e =>e.Fname)
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeBornBetween1980And1990()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var startDate = DateOnly.FromDateTime(new DateTime(1980, 1, 1));
            var endDate = DateOnly.FromDateTime(new DateTime(1990, 12, 31));

            return employees
                .Where(e => e.Dob >= startDate && e.Dob <= endDate)
                .ToList();
        }


        public async Task<IEnumerable<Employee>> GetFemaleEmployeeBornAfter1990()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var cutoffDate = DateOnly.FromDateTime(new DateTime(1990, 12, 31));

            return employees
                .Where(e => e.Sex == "Female" && e.Dob > cutoffDate)
                .OrderBy(e => e.Fname)
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetFemaleManagers()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            var employees = await _employeeRepository.GetAllEmployees();

            return employees
                .Join(departments, e => e.Empno, d => d.Mgrempno, (e, d) => e)
                .Where(e => e.Sex == "Female")
                .OrderBy(e => e.Fname)
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetNonManagerEmployees()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            var employees = await _employeeRepository.GetAllEmployees();

            var managers = departments.Select(d => d.Mgrempno).ToList();
            return employees
                .Where(e => managers.Contains(e.Empno))
                .OrderBy(e => e.Fname)
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetITDepartmentEmployeesAsync()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            var itDepartment = departments.FirstOrDefault(d => d.Deptno == 1);

            if (itDepartment == null)
            {
                return new List<Employee>(); // Return empty list if IT department is not found
            }

            var employees = await _employeeRepository.GetAllEmployees();
            return employees
                .Where(e => e.Deptno == itDepartment.Deptno)
                .OrderBy(e => e.Lname)
                .ToList();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            // Get the maximum allowed employees for the IT department from configuration
            var maxITEmployees = _configuration.GetValue<int>("CompanySettings:ITDepartmentMaxEmployee");

            // Check if the employee is being added to the IT department
            var itDepartment = await _departmentRepository.GetDepartmentByName("IT");
            if (itDepartment != null && employee.Deptno == itDepartment.Deptno)
            {
                // Count the current employees in the IT department
                var currentITEmployees = await _departmentRepository.GetEmployee(itDepartment.Deptno);

                if (currentITEmployees.Count() >= maxITEmployees)
                {
                    throw new InvalidOperationException($"The IT department already has the maximum allowed {maxITEmployees} employees.");
                }
            }

            // Add the new employee
            return await _employeeRepository.AddEmployee(employee);
        }
    }
}
