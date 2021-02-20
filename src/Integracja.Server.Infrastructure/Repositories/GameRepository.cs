using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GameRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Game> Get(int id, int userId)
        {
            return _dbContext.Games
                .AsNoTracking()
                .Where(g => g.Id == id &&
                (g.OwnerId == userId ||
                    g.Players.Any(gu => gu.UserId == userId && gu.State != GameUserState.Left)) &&
                g.GameState != GameState.Deleted);
        }

        public IQueryable<Game> GetAll(int userId)
        {
            return _dbContext.Games
                .AsNoTracking()
                .Where(g => (g.OwnerId == userId ||
                    g.Players.Any(gu => gu.UserId == userId && gu.State != GameUserState.Left)) &&
                g.GameState != GameState.Deleted);
        }

        public async Task<int> Add(Game game)
        {
            await _dbContext.AddAsync(game);
            await _dbContext.SaveChangesAsync();

            return game.Id;
        }

        public async Task Delete(Game game)
        {
            var entity = await _dbContext.Games
                .Where(g => g.Id == game.Id && g.OwnerId == game.OwnerId && g.GameState == GameState.Normal)
                .Select(g => new
                {
                    Game = g,
                    PlayersCount = g.Players.Where(p => p.State != GameUserState.Left).Count()
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.PlayersCount == 0)
            {
                entity.Game.GameState = GameState.Deleted;
            }
            else
            {
                entity.Game.GameState = GameState.Cancelled;
            }

            entity.Game.RowVersion++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Game game)
        {
            var entity = await _dbContext.Games
              .Where(g => g.Id == game.Id && g.OwnerId == game.OwnerId && g.GameState == GameState.Normal)
              .Select(g => new
              {
                  Game = g,
                  PlayersCount = g.Players.Where(p => p.State != GameUserState.Left).Count()
              })
              .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Game.Name = game.Name;
            entity.Game.StartTime = game.StartTime;
            entity.Game.EndTime = game.EndTime;
            entity.Game.MaxPlayersCount = game.MaxPlayersCount;
            entity.Game.RowVersion++;
            await _dbContext.SaveChangesAsync();

            return entity.Game.Id;
        }
    }
}
