using HR.DAL;
using HR.DAL.Models;
using HR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Configuration;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

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
            string FileName = Path.GetFileNameWithoutExtension(employee.ImageURL);
 
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + ".jpg";
            string UploadPath = ConfigurationManager.AppSettings["HRSystem/images/"].ToString();

            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(employee.ImageURL);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        yourImage.Save(UploadPath + FileName, ImageFormat.Jpeg);
                    }
                }

            }
            employee.ImageURL = UploadPath + FileName;

            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
