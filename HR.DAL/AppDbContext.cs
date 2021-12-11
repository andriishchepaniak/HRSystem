using HR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HR.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
