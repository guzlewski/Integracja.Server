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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryGet> Get(int id, int userId)
        {
            var entity = await _categoryRepository.Get(id, userId)
                .Select(c => new CategoryGet
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsPublic = c.IsPublic,
                    Questions = c.Questions.Where(q => (q.IsPublic || q.OwnerId == userId) && !q.IsDeleted).Select(q => new QuestionGetAll
                    {
                        Id = q.Id,
                        Content = q.Content,
                        PositivePoints = q.PositivePoints,
                        NegativePoints = q.NegativePoints,
                        QuestionScoring = q.QuestionScoring,
                        IsPublic = q.IsPublic,
                        AnswersCount = q.Answers.Count,
                        CorrectAnswersCount = q.Answers.Count(a => a.IsCorrect)
                    }),
                    OwnerId = c.OwnerId,
                    OwnerUsername = c.Owner.UserName
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<CategoryGetAll>> GetAll(int userId)
        {
            return await _categoryRepository.GetAll(userId)
                .Select(c => new CategoryGetAll
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsPublic = c.IsPublic,
                    QuestionsCount = c.Questions.Count(q => (q.IsPublic || q.OwnerId == userId) && !q.IsDeleted),
                    OwnerId = c.OwnerId,
                    OwnerUsername = c.Owner.UserName
                })
                .ToListAsync();
        }

        public async Task<int> Add(CategoryAdd dto, int userId)
        {
            var category = _mapper.Map<Category>(dto);
            category.OwnerId = userId;

            foreach (var question in category.Questions)
            {
                question.OwnerId = userId;
            }

            return await _categoryRepository.Add(category);
        }

        public async Task Delete(int id, int userId)
        {
            await _categoryRepository.Delete(new Category
            {
                Id = id,
                OwnerId = userId
            });
        }

        public async Task<int> Update(int id, CategoryModify dto, int userId)
        {
            var category = _mapper.Map<Category>(dto);
            category.Id = id;
            category.OwnerId = userId;

            return await _categoryRepository.Update(category);
        }
    }
}
