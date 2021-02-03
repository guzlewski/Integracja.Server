﻿using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IGamemodeRepository
    {
        IQueryable<Gamemode> Get(int id, int userId);
        IQueryable<Gamemode> GetAll(int userId);
        Task<Gamemode> Add(Gamemode gamemode);
        Task Delete(Gamemode gamemode);
        Task Update(Gamemode gamemode);
    }
}
