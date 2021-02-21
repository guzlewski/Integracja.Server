using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameUserService : IGameUserService
    {
        private readonly IGameUserRepository _gameUserRepository;

        public GameUserService(IGameUserRepository gameUserRepository)
        {
            _gameUserRepository = gameUserRepository;
        }

        public async Task Accept(int gameId, int userId)
        {
            await _gameUserRepository.Accept(gameId, userId);
        }

        public async Task<int> Join(Guid gameGuid, int userId)
        {
            return await _gameUserRepository.Join(gameGuid, userId);
        }

        public async Task Leave(int gameId, int userId)
        {
            await _gameUserRepository.Leave(gameId, userId);
        }

        public async Task<GameUserGet> Get(int gameId, int userId)
        {
            var entity = await _gameUserRepository.Get(gameId, userId)
               .Select(gu => new GameUserGet
               {
                   GameScore = gu.GameScore,
                   GameStartTime = gu.GameStartTime,
                   GameEndTime = gu.GameEndTime,
                   State = gu.State,
                   AnsweredQuestions = gu.AnsweredQuestions,
                   CorrectlyAnsweredQuestions = gu.CorrectlyAnsweredQuestions,
                   IncorrectlyAnsweredQuestions = gu.IncorrectlyAnsweredQuestions,
                   Game = new GameGet
                   {
                       Id = gu.Game.Id,
                       Guid = gu.Game.Guid,
                       Name = gu.Game.Name,
                       StartTime = gu.Game.StartTime,
                       EndTime = gu.Game.EndTime,
                       GameState = gu.Game.GameState,
                       MaxPlayersCount = gu.Game.MaxPlayersCount,
                       QuestionsCount = gu.Game.Questions.Count,
                       Gamemode = new GamemodeGet
                       {
                           Id = gu.Game.Gamemode.Id,
                           Name = gu.Game.Gamemode.Name,
                           TimeForFullQuiz = gu.Game.Gamemode.TimeForFullQuiz,
                           TimeForOneQuestion = gu.Game.Gamemode.TimeForOneQuestion.GetValueOrDefault(),
                           NumberOfLives = gu.Game.Gamemode.NumberOfLives.GetValueOrDefault(),
                           IsPublic = gu.Game.Gamemode.IsPublic,
                           OwnerId = gu.Game.Gamemode.OwnerId,
                           OwnerUsername = gu.Game.Gamemode.Owner.UserName
                       },
                       Players = gu.Game.Players
                        .Where(gu => gu.State != GameUserState.Left)
                        .Select(gu => gu.User.UserName)
                        .ToList()
                   }
               })
               .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<GameUserGetAll>> GetAll(int userId)
        {
            return await _gameUserRepository.GetAll(userId)
                 .Select(gu => new GameUserGetAll
                 {
                     GameScore = gu.GameScore,
                     GameStartTime = gu.GameStartTime,
                     GameEndTime = gu.GameEndTime,
                     State = gu.State,
                     AnsweredQuestions = gu.AnsweredQuestions,
                     CorrectlyAnsweredQuestions = gu.CorrectlyAnsweredQuestions,
                     IncorrectlyAnsweredQuestions = gu.IncorrectlyAnsweredQuestions,
                     Game = new GameGetAll
                     {
                         Id = gu.Game.Id,
                         Guid = gu.Game.Guid,
                         Name = gu.Game.Name,
                         StartTime = gu.Game.StartTime,
                         EndTime = gu.Game.EndTime,
                         GameState = gu.Game.GameState,
                         MaxPlayersCount = gu.Game.MaxPlayersCount,
                         QuestionsCount = gu.Game.Questions.Count,
                         Gamemode = new GamemodeGet
                         {
                             Id = gu.Game.Gamemode.Id,
                             Name = gu.Game.Gamemode.Name,
                             TimeForFullQuiz = gu.Game.Gamemode.TimeForFullQuiz,
                             TimeForOneQuestion = gu.Game.Gamemode.TimeForOneQuestion.GetValueOrDefault(),
                             NumberOfLives = gu.Game.Gamemode.NumberOfLives.GetValueOrDefault(),
                             IsPublic = gu.Game.Gamemode.IsPublic,
                             OwnerId = gu.Game.Gamemode.OwnerId,
                             OwnerUsername = gu.Game.Gamemode.Owner.UserName
                         },
                         PlayersCount = gu.Game.Players
                            .Where(gu => gu.State != GameUserState.Left)
                            .Count()
                     }
                 }).ToListAsync();
        }
    }
}
