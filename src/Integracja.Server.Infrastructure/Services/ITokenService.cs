using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface ITokenService
    {
        Task<TokenDTO> GenerateToken(LoginDto dto);
    }
}
