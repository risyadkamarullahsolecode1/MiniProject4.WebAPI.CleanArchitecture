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
        Task AddWorkEntryAsync(int empNo, int projNo, int hoursWorked, Workson workson);
    }
}
