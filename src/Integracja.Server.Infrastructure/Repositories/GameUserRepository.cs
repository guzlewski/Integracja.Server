using System;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Enums;
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

        public IQueryable<GameUser> Get(int gameId, int userId)
        {
            return _dbContext.GameUsers
                .AsNoTracking()
                .Where(gu => gu.GameId == gameId &&
                    gu.UserId == userId);
        }

        public IQueryable<GameUser> GetAll(int userId)
        {
            return _dbContext.GameUsers
                .AsNoTracking()
                .Where(gu => gu.UserId == userId);
        }

        public async Task<int> Join(Guid gameGuid, int userId)
        {
            var entity = await _dbContext.Games
                .Where(g => g.Guid == gameGuid &&
                    g.GameState == GameState.Normal)
                .Select(g => new
                {
                    Game = g,
                    GameUsersCount = g.GameUsers
                        .Where(gu => gu.GameUserState != GameUserState.Left)
                        .Count(),
                    GameUser = g.GameUsers
                        .Where(gu => gu.UserId == userId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.Game.EndTime <= DateTimeOffset.Now)
            {
                throw new ConflictException(ErrorCode.GameHasEnded);
            }

            if (entity.GameUser != null && entity.GameUser.GameUserState == GameUserState.Active)
            {
                throw new ConflictException(ErrorCode.AlreadyJoinedGame);
            }

            if (entity.Game.MaxPlayersCount.HasValue && entity.GameUsersCount >= entity.Game.MaxPlayersCount.Value)
            {
                throw new ConflictException(ErrorCode.GameIsFull);
            }

            if (entity.GameUser == null)
            {
                await _dbContext.AddAsync(new GameUser
                {
                    GameId = entity.Game.Id,
                    UserId = userId
                });
            }
            else
            {
                entity.GameUser.GameUserState = GameUserState.Active;
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
                    gu.GameUserState != GameUserState.Left &&
                    gu.Game.GameState == GameState.Normal)
                .Select(gu => new
                {
                    gu.Game,
                    GameUser = gu
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.Game.EndTime <= DateTimeOffset.Now)
            {
                throw new ConflictException(ErrorCode.GameHasEnded);
            }

            entity.GameUser.GameUserState = GameUserState.Left;
            entity.Game.RowVersion++;

            await _dbContext.SaveChangesAsync();
        }
    }
}
