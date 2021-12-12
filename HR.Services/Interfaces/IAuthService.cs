using System.Threading.Tasks;

namespace HR.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string username, string password);
        Task<string> RegisterNewUser(string email, string password);
    }
}
