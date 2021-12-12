using HR.DAL.Models;
using System.Threading.Tasks;

namespace HR.Services.Interfaces
{
    public interface IAdminService
    {
        Task DeleteByEmail(string email);
        Task<Employee> Update(Employee employee);
        Task<Employee> CreateNew(Employee employee);
    }
}
