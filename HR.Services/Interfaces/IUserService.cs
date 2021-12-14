using HR.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<Employee>> ShowAll();
        Task<List<Employee>> ShowAll(int offset = 0, int count = 10);
        Task<Employee> FindByEmail(string email);
        Task<List<Employee>> GetSortedByName(int offset = 0, int count = 10);
        Task<List<Employee>> GetSortedByDate(int offset = 0, int count = 10);
        Task<List<Employee>> GetSortedByFaculty(int offset = 0, int count = 10);
        Task<List<Employee>> Search(string search);
    }
}
