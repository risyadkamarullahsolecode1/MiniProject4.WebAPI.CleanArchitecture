using MiniProject4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject4.Domain.Interfaces
{
    public interface IWorksonRepository
    {
        Task<IEnumerable<Workson>> GetAllWorkOn();
        Task<Workson> GetWorkOnById(int empNo, int projNo);
        Task<Workson> AddWorkOn(Workson workOn);
        Task<Workson> UpdateWorkOn(int empNo, int projNo, Workson workson);
        Task<bool> DeleteWorkOn(int empNo, int projNo);

        //method
        Task<int> GetTotalHoursWorked(int projNo);
    }
}
