using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameQuestionService : IGameQuestionService
    {
        private readonly IGameQuestionRepository _gameQuestionRepository;
        private readonly IMapper _mapper;

        public GameQuestionService(IGameQuestionRepository gameQuestionRepository, IMapper mapper)
        {
            _gameQuestionRepository = gameQuestionRepository;
            _mapper = mapper;
        }

        public async Task<GameQuestionGet> GetQuestion(int gameId, int userId)
        {
            var entity = await _gameQuestionRepository.GetQuestion(gameId, userId);

            foreach (var answer in entity.Question.Answers)
            {
                answer.IsCorrect = false;
            }

            return _mapper.Map<GameQuestionGet>(entity);
        }

        public async Task<GameUserQuestionGet> SaveAnswers(int gameId, int userId, ICollection<int> answers)
        {
            var entity = await _gameQuestionRepository.SaveAnswers(gameId, userId, answers);
            return _mapper.Map<GameUserQuestionGet>(entity);
        }
    }
}
