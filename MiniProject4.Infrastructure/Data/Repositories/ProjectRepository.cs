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
    public class ProjectRepository:IProjectRepository
    {
        private readonly CompaniesContext _context;
        private readonly IConfiguration _configuration;

        public ProjectRepository(CompaniesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Project> AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateProject(int id, Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalHoursWorked(int projNo)
        {
            return await _context.Worksons
                .Where(w => w.Projno == projNo)
                .SumAsync(w => w.Hoursworked ?? 0);
        }

        public async Task<Department> GetDepartmentAsync(int projNo)
        {
            var department = await _context.Projects
                .Where(p => p.Projno == projNo)
                .Include(p => p.DeptnoNavigation) // Assuming DeptnoNavigation is the navigation property for the department
                .Select(p => p.DeptnoNavigation)  // Selects the department associated with the project
                .FirstOrDefaultAsync();

            return department;
        }
    }
}
