using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IGameService
    {
        Task<GameGet> Get(int id, int userId);
        Task<IEnumerable<GameGetAll>> GetAll(int userId);
        Task<int> Add(GameAdd dto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, GameModify dto, int userId);
    }
}
