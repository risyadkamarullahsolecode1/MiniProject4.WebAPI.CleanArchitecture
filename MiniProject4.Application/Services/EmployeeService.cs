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
                return employees
                .Where(e => e.Dob >= new DateTime(1980, 1, 1) && e.Dob <= new DateTime(1990, 12, 31))
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetFemaleEmployeeBornAfter1990()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return employees
                .Where(e => e.Sex == "Female" && e.Dob > new DateTime(1990, 12, 31))
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
                .Where(e => !managers.Contains(e.Empno))
                .OrderBy(e => e.Fname)
                .ToList();
        }
    }
}
