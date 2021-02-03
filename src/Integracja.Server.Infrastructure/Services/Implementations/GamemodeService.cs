using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GamemodeService : IGamemodeService
    {
        public Task<GamemodeDto> Get(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GamemodeDto>> GetAll(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GamemodeDto> Add(GamemodeDto dto, int userId)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, int userId)
        {
            throw new NotImplementedException();
        }        

        public Task Update(int id, GamemodeDto dto, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
