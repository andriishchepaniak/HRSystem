using System.Threading.Tasks;

namespace HR.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string username, string password);
        Task RegisterNewUser(string email, string password, string university, string firstname, string lastname);
    }
}
