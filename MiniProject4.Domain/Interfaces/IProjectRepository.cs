using MiniProject4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(int id, Project project);
        Task DeleteProject(int id);
        Task<Department> GetDepartmentAsync(int projNo);
    }
}
