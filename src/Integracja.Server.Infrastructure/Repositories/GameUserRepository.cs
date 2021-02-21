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

        public async Task Accept(int gameId, int userId)
        {
            var entity = await _dbContext.GameUsers
                .Where(gu => gu.GameId == gameId &&
                    gu.UserId == userId &&
                    gu.State == GameUserState.Invited)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.State = GameUserState.Active;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Join(Guid gameGuid, int userId)
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

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.GameUser != null && entity.GameUser.State != GameUserState.Left)
            {
                if (entity.GameUser.State == GameUserState.Invited)
                {
                    entity.GameUser.State = GameUserState.Active;
                    await _dbContext.SaveChangesAsync();
                    return entity.Game.Id;
                }

                throw new ConflictException("You already joined this game.");
            }

            if (entity.Game.MaxPlayersCount != 0 && entity.GameUserCount >= entity.Game.MaxPlayersCount)
            {
                throw new ConflictException($"Game '{entity.Game.Guid}' is full.");
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

            return entity.Game.Id;
        }

        public async Task Leave(int gameId, int userId)
        {
            var entity = await _dbContext.GameUsers
                .Where(gu => gu.GameId == gameId &&
                    gu.UserId == userId &&
                    gu.State != GameUserState.Left &&
                    gu.Game.GameState == GameState.Normal &&
                    gu.Game.EndTime > DateTimeOffset.Now)
                .Select(gu => new
                {
                    GameUser = gu,
                    Game = gu.Game
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.GameUser.State = GameUserState.Left;
            entity.Game.RowVersion++;
            await _dbContext.SaveChangesAsync();
        }
    }
}
