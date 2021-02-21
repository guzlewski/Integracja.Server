using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly Random _random;

        public GameService(IGameRepository gameRepository, IMapper mapper, Random random)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _random = random;
        }

        public async Task<GameGet> Get(int id, int userId)
        {
            var entity = await _gameRepository.Get(id, userId)
                .Select(game => new GameGet
                {
                    Id = game.Id,
                    Guid = game.Guid,
                    Name = game.Name,
                    StartTime = game.StartTime,
                    EndTime = game.EndTime,
                    GameState = game.GameState,
                    MaxPlayersCount = game.MaxPlayersCount,
                    QuestionsCount = game.Questions.Count,
                    Gamemode = new GamemodeGet
                    {
                        Id = game.Gamemode.Id,
                        Name = game.Gamemode.Name,
                        TimeForFullQuiz = game.Gamemode.TimeForFullQuiz,
                        TimeForOneQuestion = game.Gamemode.TimeForOneQuestion.GetValueOrDefault(),
                        NumberOfLives = game.Gamemode.NumberOfLives.GetValueOrDefault(),
                        IsPublic = game.Gamemode.IsPublic,
                        OwnerId = game.Gamemode.OwnerId,
                        OwnerUsername = game.Gamemode.Owner.UserName
                    },
                    Players = game.Players
                        .Where(gu => gu.State != GameUserState.Left)
                        .Select(gu => gu.User.UserName)
                        .ToList()
                })
               .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<GameGetAll>> GetAll(int userId)
        {
            return await _gameRepository.GetAll(userId)
                 .Select(game => new GameGetAll
                 {
                     Id = game.Id,
                     Guid = game.Guid,
                     Name = game.Name,
                     StartTime = game.StartTime,
                     EndTime = game.EndTime,
                     GameState = game.GameState,
                     MaxPlayersCount = game.MaxPlayersCount,
                     QuestionsCount = game.Questions.Count,
                     Gamemode = new GamemodeGet
                     {
                         Id = game.Gamemode.Id,
                         Name = game.Gamemode.Name,
                         TimeForFullQuiz = game.Gamemode.TimeForFullQuiz,
                         TimeForOneQuestion = game.Gamemode.TimeForOneQuestion.GetValueOrDefault(),
                         NumberOfLives = game.Gamemode.NumberOfLives.GetValueOrDefault(),
                         IsPublic = game.Gamemode.IsPublic,
                         OwnerId = game.Gamemode.OwnerId,
                         OwnerUsername = game.Gamemode.Owner.UserName
                     },
                     PlayersCount = game.Players
                        .Where(gu => gu.State != GameUserState.Left)
                        .Count()
                 })
                .ToListAsync();
        }

        public async Task<int> Add(GameAdd dto, int userId)
        {
            var game = _mapper.Map<Game>(dto);
            game.OwnerId = userId;
            game.Guid = Guid.NewGuid();

            var questions = dto.QuestionPool
                .OrderBy(gq => _random.Next())
                .Take(dto.QuestionsCount);

            game.Questions = new List<GameQuestion>();

            int index = 0;

            foreach (var gameQuestionAdd in questions)
            {
                game.Questions.Add(new GameQuestion
                {
                    Index = index++,
                    QuestionId = gameQuestionAdd.QuestionId,
                    OverridePositivePoints = gameQuestionAdd.PositivePointsOverride,
                    OverrideNegativePoints = gameQuestionAdd.NegativePointsOverride
                });
            }

            game.Players = new List<GameUser>();

            foreach (var gameUserAdd in dto.InvitedUsers)
            {
                game.Players.Add(new GameUser
                {
                    UserId = gameUserAdd.UserId
                });
            }

            return await _gameRepository.Add(game);
        }

        public async Task Delete(int id, int userId)
        {
            await _gameRepository.Delete(new Game
            {
                Id = id,
                OwnerId = userId
            });
        }

        public async Task<int> Update(int id, GameModify dto, int userId)
        {
            var game = _mapper.Map<Game>(dto);
            game.Id = id;
            game.OwnerId = userId;

            return await _gameRepository.Update(game);
        }
    }
}
