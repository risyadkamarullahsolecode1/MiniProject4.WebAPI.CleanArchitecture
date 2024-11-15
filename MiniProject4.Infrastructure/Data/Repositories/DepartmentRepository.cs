using Microsoft.EntityFrameworkCore;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Infrastructure.Data.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly CompaniesContext _context;

        public DepartmentRepository(CompaniesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> UpdateDepartment(int id, Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return ;
            
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployee(int deptNo) 
        { 
            var employeeInDept = await _context.Employees
                .Where(e => e.Deptno == deptNo)
                .ToListAsync();
            return employeeInDept;
        }
    }
}
