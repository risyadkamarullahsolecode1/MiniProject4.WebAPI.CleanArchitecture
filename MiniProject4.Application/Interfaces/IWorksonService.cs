using MiniProject4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Application.Interfaces
{
    public interface IWorksonService
    {
        Task<bool> MaxHoursEmployeeToProject(int empNo, int projNo, int hoursWorked);
        Task MaxEmployeeToProject(int empNo, int projNo, Workson workson);
    }
}
