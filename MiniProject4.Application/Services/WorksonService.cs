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

    }
}
