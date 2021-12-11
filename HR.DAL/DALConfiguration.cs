using HR.DAL.Interfaces;
using HR.DAL.Models;
using HR.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL
{
    public static class DALConfiguration
    {
        public static void ConfigureDAL(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<IBaseRepository<Employee>, BaseRepository<Employee>>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
