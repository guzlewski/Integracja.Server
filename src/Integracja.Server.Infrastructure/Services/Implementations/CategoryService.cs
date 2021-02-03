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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Create(CategoryDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new BadRequestException();
            }

            var entity = await _categoryRepository.Add(new Category
            {
                Name = dto.Name,
                IsPublic = dto.IsPublic,
                AuthorId = userId
            });

            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                IsPublic = entity.IsPublic,
                AuthorId = entity.AuthorId
            };
        }

        public async Task Delete(int id, int userId)
        {
            await _categoryRepository.Delete(new Category
            {
                Id = id,
                AuthorId = userId
            });
        }

        public async Task<CategoryDetailsDto> Get(int id, int userId)
        {
            var entity = await _categoryRepository
                .Get(id, userId)
                .Select(c => new CategoryDetailsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    AuthorId = c.AuthorId,
                    IsPublic = c.IsPublic,
                    AuthorNickname = c.Author.UserName,
                    QuestionsCount = c.Questions.Where(q => !q.IsDeleted).Count(),
                    Questions = c.Questions.Where(q => !q.IsDeleted).Select(q => new QuestionShortDto
                    {
                        Id = q.Id,
                        IsPublic = q.IsPublic,
                        Content = q.Content,
                        AnswersCount = q.Answers.Count,
                        CorrectAnswersCount = q.Answers.Where(a => a.IsCorrect).Count(),
                        PositivePoints = q.PositivePoints,
                        NegativePoints = q.NegativePoints,
                        QuestionScoring = q.QuestionScoring
                    })
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll(int userId)
        {
            var entities = await _categoryRepository.GetAll(userId)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    AuthorId = c.AuthorId,
                    IsPublic = c.IsPublic,
                    AuthorNickname = c.Author.UserName,
                    QuestionsCount = c.Questions.Where(q => !q.IsDeleted).Count()
                })
                .ToListAsync();

            return entities;
        }

        public async Task Update(int id, CategoryDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new BadRequestException();
            }

            var category = new Category
            {
                Id = id,
                AuthorId = userId,
                Name = dto.Name,
                IsPublic
                = dto.IsPublic
            };

            await _categoryRepository.Update(category);
        }
    }
}
