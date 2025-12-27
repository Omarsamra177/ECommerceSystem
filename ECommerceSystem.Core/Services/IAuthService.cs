using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string password, UserRole role);
        Task<string> LoginAsync(string email, string password);
    }
}
