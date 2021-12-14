using HR.DAL;
using HR.DAL.Models;
using HR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HR.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> FindByEmail(string email)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<List<Employee>> GetSortedByDate(int offset = 0, int count = 10)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .OrderBy(e => e.DateOfBirth)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetSortedByFaculty(int offset = 0, int count = 10)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .OrderBy(e => e.Faculty)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetSortedByName(int offset = 0, int count = 10)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Employee>> Search(string search)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .Where(e => e.LastName.ToLower().Contains(search.ToLower()) 
                    || e.FirstName.ToLower().Contains(search.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Employee>> ShowAll()
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .ToListAsync();
        }

        public async Task<List<Employee>> ShowAll(int offset = 0, int count = 10)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }
    }
}
