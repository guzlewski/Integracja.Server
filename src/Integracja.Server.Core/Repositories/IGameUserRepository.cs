using System;
using System.Threading.Tasks;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameUserRepository
    {
        Task Join(Guid gameGuid, int userId);
        Task Leave(Guid gameGuid, int userId);
    }
}
