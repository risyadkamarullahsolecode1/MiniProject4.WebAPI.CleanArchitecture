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
    public class WorksonRepository:IWorksonRepository
    {
        private readonly CompaniesContext _context;

        public WorksonRepository(CompaniesContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Workson>> GetAllWorkOn()
        {
            return await _context.Worksons.ToListAsync();
        }

        public async Task<Workson> GetWorkOnById(int empNo, int projNo)
        {
            return await _context.Worksons.FindAsync(empNo, projNo);
        }

        public async Task<Workson> AddWorkOn(Workson workson)
        {
            await _context.Worksons.AddAsync(workson);
            await _context.SaveChangesAsync();
            return workson;
        }

        public async Task<Workson> UpdateWorkOn(int empNo, int projNo, Workson workson)
        {
            _context.Worksons.Update(workson);
            await _context.SaveChangesAsync();
            return workson;
        }

        public async Task<bool> DeleteWorkOn(int empNo, int projNo)
        {
            var workson = await _context.Worksons.FindAsync(empNo, projNo);
            if (workson == null)
            {
                return false;
            }

            _context.Worksons.Remove(workson);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalHoursWorked(int projNo)
        {
            return await _context.Worksons
                .Where(w => w.Projno == projNo)
                .SumAsync(w => w.Hoursworked ?? 0);
        }
    }
}
