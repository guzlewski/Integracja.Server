using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameLogicService : IGameLogicService
    {
        private readonly IGameLogicRepository _gameQuestionRepository;
        private readonly IConfigurationProvider _configuration;

        public GameLogicService(IGameLogicRepository gameQuestionRepository, IConfigurationProvider configuration)
        {
            _gameQuestionRepository = gameQuestionRepository;
            _configuration = configuration;
        }

        public async Task<T> GetQuestion<T>(int gameId, int userId)
        {
            var dto = await (await _gameQuestionRepository.GetQuestion(gameId, userId))
                .ProjectTo<T>(_configuration)
                .FirstOrDefaultAsync();

            if (dto == null)
            {
                throw new NotFoundException();
            }

            return dto;
        }

        public async Task<T> SaveAnswers<T>(int gameId, int userId, int questionId, IEnumerable<int> answers)
        {
            var dto = await (await _gameQuestionRepository.SaveAnswers(gameId, userId, questionId, answers))
                .ProjectTo<T>(_configuration)
                .FirstOrDefaultAsync();

            if (dto == null)
            {
                throw new NotFoundException();
            }

            return dto;
        }
    }
}
