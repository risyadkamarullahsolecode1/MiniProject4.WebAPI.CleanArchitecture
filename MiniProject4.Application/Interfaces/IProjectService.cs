using MiniProject4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Application.Interfaces
{
    public interface IProjectService
    {
        

        Task<IEnumerable<Project>> GetProjectsManagedByPlanning();
        Task<IEnumerable<Project>> GetProjectsWithNoEmployees();
        Task<IEnumerable<object>> GetProjectsManagedByFemaleManagers();
    }
}
