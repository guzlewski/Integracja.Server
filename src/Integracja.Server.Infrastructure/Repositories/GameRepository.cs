using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.EqualityComparers;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Random _random;

        public GameRepository(ApplicationDbContext dbContext, Random random)
        {
            _dbContext = dbContext;
            _random = random;
        }

        public IQueryable<Game> Get(int id)
        {
            return _dbContext.Games
                .AsNoTracking()
                .Where(g => g.Id == id);
        }

        public IQueryable<Game> GetAll()
        {
            return _dbContext.Games
                .AsNoTracking();
        }

        public async Task<int> Add(Game game, bool randomizeQuestionOrder = false)
        {
            var gamemodeEntity = await _dbContext.Gamemodes
                .FirstOrDefaultAsync(gm => gm.Id == game.GamemodeId &&
                    (gm.IsPublic || gm.OwnerId == game.OwnerId) &&
                    !gm.IsDeleted);

            if (gamemodeEntity == null)
            {
                throw new NotFoundException("Gamemode not found.");
            }

            gamemodeEntity.RowVersion++;

            var ids = game.Questions.Select(gq => gq.QuestionId);
            var entities = await _dbContext.Questions
                .Where(q => !q.IsDeleted &&
                    (q.IsPublic || q.OwnerId == game.OwnerId) &&
                    ids.Contains(q.Id))
                .Select(q => new GameQuestion
                {
                    QuestionId = q.Id,
                    Question = q
                })
                .ToListAsync();

            if (entities.Count == 0)
            {
                throw new BadRequestException("None question from pool is available.");
            }

            game.Questions = SelectQuestions(game.Questions, entities, game.QuestionsCount, randomizeQuestionOrder);
            game.QuestionsCount = game.Questions.Count;

            ids = game.GameUsers.Select(gu => gu.UserId);
            game.GameUsers = await _dbContext.Users
                .AsNoTracking()
                .Where(u => !u.IsDeleted &&
                    ids.Contains(u.Id))
                .Select(u => new GameUser
                {
                    UserId = u.Id
                })
                .ToListAsync();

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
                    PlayersCount = g.GameUsers.Where(p => p.GameUserState != GameUserState.Left).Count()
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
                  PlayersCount = g.GameUsers.Where(p => p.GameUserState == GameUserState.Active).Count()
              })
              .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.Game.StartTime <= DateTimeOffset.Now && entity.PlayersCount != 0)
            {
                throw new ConflictException("Game is in progress and has active players, can't edit.");
            }

            entity.Game.RowVersion++;
            UpdateGame(entity.Game, game);

            await _dbContext.SaveChangesAsync();
            return entity.Game.Id;
        }

        private static void UpdateGame(Game orginal, Game modified)
        {
            orginal.Name = modified.Name;
            orginal.StartTime = modified.StartTime;
            orginal.EndTime = modified.EndTime;
            orginal.MaxPlayersCount = modified.MaxPlayersCount;
        }

        private ICollection<GameQuestion> SelectQuestions(ICollection<GameQuestion> gameQuestionsPool, ICollection<GameQuestion> gameQuestionsRemote, int questionsCount, bool randomizeQuestionOrder)
        {
            var poolSet = new HashSet<GameQuestion>(gameQuestionsPool, new GameQuestionEqualityComparer());
            var selectedRemote = RandAndTake(gameQuestionsRemote, randomizeQuestionOrder, questionsCount);
            var selected = new List<GameQuestion>();
            var index = 0;

            foreach (var question in selectedRemote)
            {
                poolSet.TryGetValue(question, out var questionPool);

                questionPool.Question = question.Question;
                questionPool.Question.RowVersion++;
                questionPool.Index = index++;

                selected.Add(questionPool);
            }

            return selected;
        }

        private IEnumerable<T> RandAndTake<T>(IEnumerable<T> source, bool random, int count)
        {
            if (random)
            {
                return source.OrderBy(o => _random.Next()).Take(count);
            }

            return source.Take(count);
        }
    }
}
