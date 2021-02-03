using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class GamemodeRepository : IGamemodeRepository
    {
        public IQueryable<Gamemode> Get(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Gamemode> GetAll(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Gamemode> Add(Gamemode gamemode)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Gamemode gamemode)
        {
            throw new NotImplementedException();
        }        

        public Task Update(Gamemode gamemode)
        {
            throw new NotImplementedException();
        }
    }
}
