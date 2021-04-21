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

        public IQueryable<Gamemode> Get(int id)
        {
            return _dbContext.Gamemodes
                .AsNoTracking()
                .Where(gm => gm.Id == id);
        }

        public IQueryable<Gamemode> GetAll()
        {
            return _dbContext.Gamemodes
                .AsNoTracking();
        }

        public async Task<int> Add(Gamemode gamemode)
        {
            await _dbContext.AddAsync(gamemode);
            await _dbContext.SaveChangesAsync();

            return gamemode.Id;
        }

        public async Task Delete(Gamemode gamemode, bool skipUserVerification = false)
        {
            var gamemodeEntity = await _dbContext.Gamemodes
                .FirstOrDefaultAsync(gm => gm.Id == gamemode.Id &&
                    (gm.OwnerId == gamemode.OwnerId || skipUserVerification) &&
                    !gm.IsDeleted);

            if (gamemodeEntity == null)
            {
                throw new NotFoundException();
            }

            gamemodeEntity.IsDeleted = true;
            gamemodeEntity.RowVersion++;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Gamemode gamemode, bool skipUserVerification = false)
        {
            var entity = await _dbContext.Gamemodes
                .Where(gm => gm.Id == gamemode.Id &&
                    (gm.OwnerId == gamemode.OwnerId || skipUserVerification) &&
                    !gm.IsDeleted)
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
                UpdateGamemode(entity.Gamemode, gamemode);

                await _dbContext.SaveChangesAsync();
                return entity.Gamemode.Id;
            }
            else
            {
                gamemode.Id = 0;
                entity.Gamemode.IsDeleted = true;

                return await Add(gamemode);
            }
        }

        private static void UpdateGamemode(Gamemode orginal, Gamemode modified)
        {
            orginal.Name = modified.Name;
            orginal.TimeForFullQuiz = modified.TimeForFullQuiz;
            orginal.TimeForOneQuestion = modified.TimeForOneQuestion;
            orginal.NumberOfLives = modified.NumberOfLives;
            orginal.IsPublic = modified.IsPublic;
        }
    }
}
