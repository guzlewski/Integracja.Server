using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDetailsDto> Get(int id, int userId)
        {
            var entity = await _questionRepository.Get(id, userId)
              .Select(q => new QuestionDetailsDto
              {
                  Id = q.Id,
                  IsPublic = q.IsPublic,
                  Content = q.Content,
                  AnswersCount = q.Answers.Count,
                  CorrectAnswersCount = q.Answers.Where(a => a.IsCorrect).Count(),
                  PositivePoints = q.PositivePoints,
                  NegativePoints = q.NegativePoints,
                  AuthorId = q.AuthorId,
                  CategoryId = q.CategoryId,
                  Answers = q.Answers.Select(a => new AnswerDto
                  {
                      Content = a.Content,
                      IsCorrect = a.IsCorrect
                  })
              })
              .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<QuestionDto>> GetAll(int userId)
        {
            return await _questionRepository.GetAll(userId)
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    IsPublic = q.IsPublic,
                    Content = q.Content,
                    AnswersCount = q.Answers.Count,
                    CorrectAnswersCount = q.Answers.Where(a => a.IsCorrect).Count(),
                    PositivePoints = q.PositivePoints,
                    NegativePoints = q.NegativePoints,
                    AuthorId = q.AuthorId,
                    CategoryId = q.CategoryId
                }).ToListAsync();
        }

        public async Task<QuestionDetailsDto> Add(QuestionDetailsDto dto, int userId)
        {
            var entity = await _questionRepository.Add(new Question
            {
                IsPublic = dto.IsPublic,
                AuthorId = userId,
                Content = dto.Content,
                PositivePoints = dto.PositivePoints,
                NegativePoints = dto.NegativePoints,
                QuestionScoring = dto.QuestionScoring,
                CategoryId = dto.CategoryId,
                Answers = dto.Answers.Select(a => new Answer
                {
                    Content = a.Content,
                    IsCorrect = a.IsCorrect
                }).ToList()
            });

            return new QuestionDetailsDto
            {
                Id = entity.Id,
                IsPublic = entity.IsPublic,
                AuthorId = entity.AuthorId,
                Content = entity.Content,
                PositivePoints = entity.PositivePoints,
                NegativePoints = entity.NegativePoints,
                QuestionScoring = entity.QuestionScoring,
                CategoryId = entity.CategoryId,
                Answers = entity.Answers.Select(a => new AnswerDto { Content = a.Content, IsCorrect = a.IsCorrect })
            };
        }

        public async Task Delete(int id, int userId)
        {
            await _questionRepository.Delete(new Question
            {
                Id = id,
                AuthorId = userId
            });
        }

        public async Task Update(int id, QuestionDetailsDto dto, int userId)
        {
            await _questionRepository.Update(new Question
            {
                Id = id,
                IsPublic = dto.IsPublic,
                AuthorId = userId,
                Content = dto.Content,
                PositivePoints = dto.PositivePoints,
                NegativePoints = dto.NegativePoints,
                QuestionScoring = dto.QuestionScoring,
                CategoryId = dto.CategoryId,
                Answers = dto.Answers.Select(a => new Answer
                {
                    Content = a.Content,
                    IsCorrect = a.IsCorrect
                }).ToList()
            });
        }
    }
}
