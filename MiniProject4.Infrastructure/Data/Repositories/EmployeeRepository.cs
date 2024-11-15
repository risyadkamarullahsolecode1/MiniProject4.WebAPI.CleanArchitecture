using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Infrastructure.Data.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly CompaniesContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(CompaniesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            //check is employee age is not 65 or above
            var retirementAge = _configuration.GetValue<int>("CompanySettings:RetireEmployee");
            var age = DateTime.Now.Year - employee.Dob.Value.Year;

            if (age >= retirementAge)
                throw new InvalidOperationException("An employee who is 65 or older cannot be hired.");

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployee(int empNo)
        {
            var employee = await _context.Employees.FindAsync(empNo);
            if (employee == null)
            {
                return false;
            }

            var departments = _context.Departments.Where(d => d.Mgrempno == empNo).ToList();
            foreach (var department in departments)
            {
                department.Mgrempno = null;
            }

            _context.Departments.UpdateRange(departments);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetEmployeesByProjectAsync(int projNo)
        {
            var employeeProjectDetails = await _context.Worksons
                .Where(w => w.Projno == projNo)
                .Include(w => w.EmpnoNavigation)  // Include Employee details
                .Include(w => w.ProjnoNavigation) // Include Project details
                .Select(w => new
                {
                    EmployeeNo = w.EmpnoNavigation.Empno,
                    EmployeeName = $"{w.EmpnoNavigation.Fname} {w.EmpnoNavigation.Lname}",
                    ProjectNo = w.Projno,
                    ProjectName = w.ProjnoNavigation.Projname,
                    TotalHours = w.Hoursworked,
                    DateWorked = w.Dateworked
                })
                .ToListAsync();

            return employeeProjectDetails;
        }
    }
}
