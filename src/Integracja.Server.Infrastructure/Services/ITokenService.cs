using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface ITokenService
    {
        Task<TokenDto> GenerateToken(LoginDto dto);
    }
}
