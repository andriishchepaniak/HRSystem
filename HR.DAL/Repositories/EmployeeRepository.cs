using HR.DAL.Interfaces;
using HR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public override async Task<IEnumerable<Employee>> GetAll()
        {
            return await db.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .ToListAsync();
        }
        public override async Task<IEnumerable<Employee>> GetAllWithPage(int offset, int count)
        {
            return await db.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }
        public override async Task<Employee> GetById(int id)
        {
            return await db.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
