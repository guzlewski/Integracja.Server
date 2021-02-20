using System;
using System.Threading.Tasks;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IGameUserService
    {
        Task Join(Guid gameGuid, int userId);
        Task Leave(Guid gameGuid, int userId);
    }
}
