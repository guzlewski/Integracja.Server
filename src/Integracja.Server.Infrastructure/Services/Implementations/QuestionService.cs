using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<QuestionGet> Get(int id, int userId)
        {
            var entity = await _questionRepository.Get(id, userId)
                .Select(q => new QuestionGet
                {
                    Id = q.Id,
                    Content = q.Content,
                    PositivePoints = q.PositivePoints,
                    NegativePoints = q.NegativePoints,
                    QuestionScoring = q.QuestionScoring,
                    IsPublic = q.IsPublic,
                    CategoryId = q.CategoryId,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    }),
                    OwnerId = q.OwnerId,
                    OwnerUsername = q.Owner.UserName
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<QuestionGetAll>> GetAll(int userId)
        {
            return await _questionRepository.GetAll(userId)
                .Select(q => new QuestionGetAll
                {
                    Id = q.Id,
                    Content = q.Content,
                    PositivePoints = q.PositivePoints,
                    NegativePoints = q.NegativePoints,
                    QuestionScoring = q.QuestionScoring,
                    IsPublic = q.IsPublic,
                    CategoryId = q.CategoryId,
                    AnswersCount = q.Answers.Count,
                    CorrectAnswersCount = q.Answers.Count(a => a.IsCorrect),
                    OwnerId = q.OwnerId,
                    OwnerUsername = q.Owner.UserName
                })
                .ToListAsync();
        }

        public async Task<int> Add(QuestionAdd dto, int userId)
        {
            var question = _mapper.Map<Question>(dto);
            question.OwnerId = userId;

            return await _questionRepository.Add(question);
        }

        public async Task Delete(int id, int userId)
        {
            await _questionRepository.Delete(new Question
            {
                Id = id,
                OwnerId = userId
            });
        }

        public async Task<int> Update(int id, QuestionModify dto, int userId)
        {
            var question = _mapper.Map<Question>(dto);
            question.Id = id;
            question.OwnerId = userId;

            return await _questionRepository.Update(question);
        }
    }
}
