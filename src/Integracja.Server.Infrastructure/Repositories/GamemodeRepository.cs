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
                .Where(gm => gm.Id == id && (gm.IsPublic || gm.AuthorId == userId) && !gm.IsDeleted);

            return entity;
        }

        public IQueryable<Gamemode> GetAll(int userId)
        {
            var entities = _dbContext.Gamemodes
                .AsNoTracking()
                .Where(gm => (gm.IsPublic || gm.AuthorId == userId) && !gm.IsDeleted);

            return entities;
        }

        public async Task<Gamemode> Add(Gamemode gamemode)
        {
            gamemode.Id = 0;

            await _dbContext.AddAsync(gamemode);
            await _dbContext.SaveChangesAsync();

            return gamemode;
        }

        public async Task Delete(Gamemode gamemode)
        {
            var entity = await _dbContext.Gamemodes
                .Where(gm => gm.Id == gamemode.Id && gm.AuthorId == gamemode.AuthorId && !gm.IsDeleted)
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

        public async Task Update(Gamemode gamemode)
        {
            var entity = await _dbContext.Gamemodes
               .FirstOrDefaultAsync(gm => gm.Id == gamemode.Id && gm.AuthorId == gamemode.AuthorId && !gm.IsDeleted);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Name = gamemode.Name;
            entity.TimeForFullQuiz = gamemode.TimeForFullQuiz;
            entity.TimeForOneQuestion = gamemode.TimeForOneQuestion;
            entity.NumberOfLives = gamemode.NumberOfLives;
            entity.IsPublic = gamemode.IsPublic;
            entity.RowVersion++;

            await _dbContext.SaveChangesAsync();
        }
    }
}
