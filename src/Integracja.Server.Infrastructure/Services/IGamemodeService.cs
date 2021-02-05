using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IGamemodeService
    {
        Task<GamemodeGet> Get(int id, int userId);
        Task<IEnumerable<GamemodeGetAll>> GetAll(int userId);
        Task<int> Add(GamemodeAdd dto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, GamemodeModify dto, int userId);
    }
}
