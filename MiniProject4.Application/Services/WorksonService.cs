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
    public class WorksonService:IWorksonService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorksonRepository _worksonRepository;
        private readonly IConfiguration _configuration;

        public WorksonService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IProjectRepository projectRepository,IWorksonRepository worksonRepository , IConfiguration configuration)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _worksonRepository = worksonRepository;
            _configuration = configuration;
        }

        public async Task<bool> MaxHoursEmployeeToProject(int empNo, int projNo, int hoursWorked)
        {
            var project = await _projectRepository.GetProjectById(empNo);
            if (project == null)
            {
                throw new ArgumentException("Project not found");
            }

            var maxWorkingHours = _configuration.GetValue<int>("CompanySettings:WorkingHours");
            var workson = await _worksonRepository.GetWorkOnById(projNo, hoursWorked);

            if (workson.Hoursworked == null) 
                throw new InvalidOperationException("Project current hours are not set.");
            if (workson.Hoursworked + hoursWorked > maxWorkingHours)
                throw new Exception("This project cannot exceeds more than 600 hours");

            await _worksonRepository.UpdateWorkOn(empNo, projNo, workson);
            return true;
        }

        public async Task MaxEmployeeToProject(int empNo, int projNo, Workson workson)
        {
            var maxProjects = _configuration.GetValue<int>("CompanySettings:EmployeePerProject");
            var employee = await _employeeRepository.GetEmployeeById(empNo);

            if(employee.Worksons.Count >= maxProjects)
                throw new InvalidOperationException("An employee cannot be assigned to more than 3 projects.");

            await _worksonRepository.AddWorkOn(workson);
            return ;
        }
    }
}
