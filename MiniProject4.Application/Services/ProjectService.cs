using Microsoft.Extensions.Configuration;
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
    public class ProjectService:IProjectService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorksonRepository _worksonRepository;
        private readonly IConfiguration _configuration;
        public ProjectService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IProjectRepository projectRepository, IWorksonRepository worksonRepository, IConfiguration configuration)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _worksonRepository = worksonRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Project>> GetProjectsManagedByPlanning()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            var projects = await _projectRepository.GetAllProjects();

            var planningDept = departments.FirstOrDefault(d => d.Deptname == "Planning");

            if (planningDept == null)
            {
                return new List<Project>();
            }

            return projects
                .Where(p => p.Deptno == planningDept.Deptno)
                .ToList();
        }
        //method Get project with no employee
        public async Task<IEnumerable<Project>> GetProjectsWithNoEmployees()
        {
            var projects = await _projectRepository.GetAllProjects();
            var worksons = await _worksonRepository.GetAllWorkOn();

            var projectWithEmployee = worksons
                .Select(wo => wo.Projno).Distinct().ToList();

            return projects
            .Where(p => !projectWithEmployee.Contains(p.Projno))
            .ToList();
        }
        public async Task<IEnumerable<object>> GetProjectsManagedByFemaleManagers()
        {
            var projects = await _projectRepository.GetAllProjects();
            var employees = await _employeeRepository.GetAllEmployees();
            var departments = await _departmentRepository.GetAllDepartments();

            return employees
                .Join(departments, e => e.Empno, d => d.Mgrempno, (e, d) => new { e, d })
                .Where(x => x.e.Sex == "Female")
                .Join(projects, x => x.d.Deptno, p => p.Deptno, (x, p) => new { x.e.Fname, x.e.Lname, p.Projname })
                .OrderBy(x => x.Fname)
                .OrderBy(x => x.Lname)
                .OrderBy(x => x.Projname)
                .ToList();
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            // Get the maximum allowed projects per department from configuration
            var maxProjectsPerDepartment = _configuration.GetValue<int>("CompanySettings:MaxProjectPerDepartment");

            // Get the current count of projects in the department
            var existingProjectsCount = await _projectRepository.GetProjectsByDepartmentId(project.Deptno);

            if (existingProjectsCount.Count() >= maxProjectsPerDepartment)
            {
                throw new InvalidOperationException($"The department with ID {project.Deptno} already has the maximum allowed {maxProjectsPerDepartment} projects.");
            }

            // Add the new project
            return await _projectRepository.AddProject(project);
        }
    }
}
