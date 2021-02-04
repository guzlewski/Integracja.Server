using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Question> Get(int id, int userId)
        {
            var entity = _dbContext.Questions
                .AsNoTracking()
                .Where(q => q.Id == id &&
                (q.IsPublic || q.OwnerId == userId) &&
                (q.Category.IsPublic || q.Category.OwnerId == userId)
                && !q.IsDeleted && !q.Category.IsDeleted);

            return entity;
        }

        public IQueryable<Question> GetAll(int userId)
        {
            var entities = _dbContext.Questions
                .AsNoTracking()
                .Where(q => (q.IsPublic || q.OwnerId == userId) &&
                (q.Category.IsPublic || q.Category.OwnerId == userId) &&
                !q.IsDeleted && !q.Category.IsDeleted);

            return entities;
        }

        public async Task<Question> Add(Question question)
        {
            question.Id = 0;

            var categoryOwner = await _dbContext.Categories
                .Where(c => c.Id == question.CategoryId && !c.IsDeleted)
                .Select(c => c.OwnerId)
                .FirstOrDefaultAsync();

            if (categoryOwner != question.OwnerId)
            {
                throw new ForbiddenException($"{categoryOwner} != {question.OwnerId}, {question.CategoryId}");
            }

            await _dbContext.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            return question;
        }

        public async Task Delete(Question question)
        {
            var entity = await _dbContext.Questions
                .Where(q => q.Id == question.Id && q.OwnerId == question.OwnerId && !q.IsDeleted)
                .Select(q => new
                {
                    Question = q,
                    GameQuestionsCount = q.GameQuestions.Count,
                    Category = q.Category
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.GameQuestionsCount == 0)
            {
                _dbContext.Remove(entity.Question);
            }
            else
            {
                entity.Question.IsDeleted = true;
                entity.Question.RowVersion++;
            }

            entity.Category.RowVersion++;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Question question)
        {
            var entity = await _dbContext.Questions
               .Include(q => q.Answers)
               .Where(q => q.Id == question.Id && q.OwnerId == question.OwnerId && !q.IsDeleted)
               .Select(q => new
               {
                   Question = q,
                   GameQuestionsCount = q.GameQuestions.Count
               })
               .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Question.RowVersion++;

            if (entity.GameQuestionsCount == 0)
            {
                entity.Question.Content = question.Content;
                entity.Question.PositivePoints = question.PositivePoints;
                entity.Question.NegativePoints = question.NegativePoints;
                entity.Question.QuestionScoring = question.QuestionScoring;
                entity.Question.IsPublic = question.IsPublic;
                entity.Question.Answers = question.Answers;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                entity.Question.IsDeleted = true;
                await Add(question);
            }
        }
    }
}
