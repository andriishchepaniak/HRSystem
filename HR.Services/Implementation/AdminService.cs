using HR.DAL;
using HR.DAL.Models;
using HR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HR.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateNew(Employee employee)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(e => e.RoleName == employee.Role.RoleName);

            employee.Role = role;
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteByEmail(string email)
        {
            var entity = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> Update(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
