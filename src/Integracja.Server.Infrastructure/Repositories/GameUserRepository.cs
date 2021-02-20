using System;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class GameUserRepository : IGameUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GameUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Join(Guid gameGuid, int userId)
        {
            var entity = await _dbContext.Games
                .Where(g => g.Guid == gameGuid && g.GameState == GameState.Normal && g.EndTime > DateTimeOffset.Now)
                .Select(g => new
                {
                    Game = g,
                    GameUserCount = g.Players
                        .Where(gu => gu.State != GameUserState.Left)
                        .Count(),
                    GameUser = g.Players
                        .Where(gu => gu.UserId == userId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (entity.Game == null)
            {
                throw new NotFoundException();
            }

            if (entity.Game.MaxPlayersCount != 0 && entity.GameUserCount >= entity.Game.MaxPlayersCount)
            {
                throw new ApiException(500, $"Game '{entity.Game.Name}' is full.");
            }

            if (entity.GameUser == null)
            {
                await _dbContext.AddAsync(new GameUser
                {
                    GameId = entity.Game.Id,
                    UserId = userId,
                    State = GameUserState.Active
                });
            }
            else
            {
                entity.GameUser.State = GameUserState.Active;
            }

            entity.Game.RowVersion++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Leave(Guid gameGuid, int userId)
        {
            var entity = await _dbContext.Games
                .Where(g => g.Guid == gameGuid && g.GameState == GameState.Normal && g.EndTime > DateTimeOffset.Now)
                .Select(g => new
                {
                    Game = g,
                    GameUser = g.Players
                        .Where(gu => gu.UserId == userId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (entity == null || entity.GameUser == null)
            {
                throw new NotFoundException();
            }

            entity.GameUser.State = GameUserState.Left;
            entity.Game.RowVersion++;
            await _dbContext.SaveChangesAsync();
        }
    }
}
