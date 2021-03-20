using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameUserService : IGameUserService
    {
        private readonly IGameUserRepository _gameUserRepository;
        private readonly IConfigurationProvider _configuration;

        public GameUserService(IGameUserRepository gameUserRepository, IConfigurationProvider configuration)
        {
            _gameUserRepository = gameUserRepository;
            _configuration = configuration;
        }

        public async Task<T> Get<T>(int gameId, int userId)
        {
            var dto = await _gameUserRepository.Get(gameId, userId)
                .Where(gu => gu.GameUserState != GameUserState.Left &&
                    gu.Game.GameState != GameState.Deleted)
                .ProjectTo<T>(_configuration)
                .FirstOrDefaultAsync();

            if (dto == null)
            {
                throw new NotFoundException();
            }

            return dto;
        }

        public async Task<IEnumerable<T>> GetActive<T>(int userId)
        {
            return await _gameUserRepository.GetAll(userId)
                .Where(gu => gu.GameUserState != GameUserState.Left &&
                    gu.Game.GameState != GameState.Deleted &&
                    gu.Game.EndTime > DateTimeOffset.Now &&
                    gu.AnsweredQuestions != gu.Game.QuestionsCount &&
                    !gu.GameOver)
                .ProjectTo<T>(_configuration)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetArchived<T>(int userId)
        {
            return await _gameUserRepository.GetAll(userId)
                .Where(gu => gu.GameUserState != GameUserState.Left &&
                    gu.Game.GameState != GameState.Deleted &&
                    (gu.Game.EndTime <= DateTimeOffset.Now ||
                    gu.AnsweredQuestions == gu.Game.QuestionsCount ||
                    gu.GameOver))
                .ProjectTo<T>(_configuration)
                .ToListAsync();
        }

        public async Task<int> Join(Guid gameGuid, int userId)
        {
            return await _gameUserRepository.Join(gameGuid, userId);
        }

        public async Task Leave(int gameId, int userId)
        {
            await _gameUserRepository.Leave(gameId, userId);
        }
    }
}
