using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public interface IAuthService
    {
        Task<bool> Login(string email, string password);
        Task Logout();
        Task<User?> GetCurrentUser();
    }
}
