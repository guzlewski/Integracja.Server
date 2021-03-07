using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginDto dto);
        Task Logout(int userId);
    }
}
