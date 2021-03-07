using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginDto dto);
        Task Logout(int userId);
    }
}
