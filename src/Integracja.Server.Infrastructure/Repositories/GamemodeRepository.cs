using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class GamemodeRepository : IGamemodeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GamemodeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Gamemode> Get(int id, int userId)
        {
            var entity = _dbContext.Gamemodes
                .AsNoTracking()
                .Where(gm => gm.Id == id && (gm.IsPublic || gm.OwnerId == userId) && !gm.IsDeleted);

            return entity;
        }

        public IQueryable<Gamemode> GetAll(int userId)
        {
            var entities = _dbContext.Gamemodes
                .AsNoTracking()
                .Where(gm => (gm.IsPublic || gm.OwnerId == userId) && !gm.IsDeleted);

            return entities;
        }

        public async Task<int> Add(Gamemode gamemode)
        {
            await _dbContext.AddAsync(gamemode);
            await _dbContext.SaveChangesAsync();

            return gamemode.Id;
        }

        public async Task Delete(Gamemode gamemode)
        {
            var entity = await _dbContext.Gamemodes
                .Where(gm => gm.Id == gamemode.Id && gm.OwnerId == gamemode.OwnerId && !gm.IsDeleted)
                .Select(gm => new
                {
                    Gamemode = gm,
                    GamesCount = gm.Games.Count
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.GamesCount == 0)
            {
                _dbContext.Remove(entity.Gamemode);
            }
            else
            {
                entity.Gamemode.IsDeleted = true;
                entity.Gamemode.RowVersion++;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Gamemode gamemode)
        {
            var entity = await _dbContext.Gamemodes
                .Where(gm => gm.Id == gamemode.Id && gm.OwnerId == gamemode.OwnerId && !gm.IsDeleted)
                .Select(gm => new
                {
                    Gamemode = gm,
                    GamesCount = gm.Games.Count
                })
               .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Gamemode.RowVersion++;

            if (entity.GamesCount == 0)
            {
                entity.Gamemode.Name = gamemode.Name;
                entity.Gamemode.TimeForFullQuiz = gamemode.TimeForFullQuiz;
                entity.Gamemode.TimeForOneQuestion = gamemode.TimeForOneQuestion;
                entity.Gamemode.NumberOfLives = gamemode.NumberOfLives;
                entity.Gamemode.IsPublic = gamemode.IsPublic;

                await _dbContext.SaveChangesAsync();
                return entity.Gamemode.Id;
            }
            else
            {
                entity.Gamemode.IsDeleted = true;
                gamemode.Id = 0;

                return await Add(gamemode);
            }
        }
    }
}
