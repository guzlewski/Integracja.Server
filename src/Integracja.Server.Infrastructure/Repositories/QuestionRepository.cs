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
            return _dbContext.Questions
                .AsNoTracking()
                .Where(q => q.Id == id &&
                (q.IsPublic || q.OwnerId == userId) && !q.IsDeleted &&
                (q.Category.IsPublic || q.Category.OwnerId == userId) && !q.Category.IsDeleted);
        }

        public IQueryable<Question> GetAll(int userId)
        {
            return _dbContext.Questions
                .AsNoTracking()
                .Where(q => (q.IsPublic || q.OwnerId == userId) && !q.IsDeleted &&
                (q.Category.IsPublic || q.Category.OwnerId == userId) && !q.Category.IsDeleted);
        }

        public async Task<int> Add(Question question)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == question.CategoryId && !c.IsDeleted);

            if (category == null)
            {
                throw new NotFoundException($"Category with ID {question.CategoryId} not exists.");
            }

            if (category.OwnerId != question.OwnerId)
            {
                throw new ForbiddenException();
            }

            category.RowVersion++;

            await _dbContext.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            return question.Id;
        }

        public async Task Delete(Question question)
        {
            var entity = await _dbContext.Questions
                .Where(q => q.Id == question.Id && q.OwnerId == question.OwnerId && !q.IsDeleted)
                .Select(q => new
                {
                    Question = q,
                    Category = q.Category,
                    GameQuestionsCount = q.GameQuestions.Count
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
            }

            entity.Category.RowVersion++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Question question)
        {
            var entity = await _dbContext.Questions
               .Include(q => q.Answers)
               .Where(q => q.Id == question.Id && q.OwnerId == question.OwnerId && !q.IsDeleted && !q.Category.IsDeleted)
               .Select(q => new
               {
                   Question = q,
                   Category = q.Category,
                   GameQuestionsCount = q.GameQuestions.Count
               })
               .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Question.RowVersion++;
            entity.Category.RowVersion++;

            if (entity.GameQuestionsCount == 0)
            {
                entity.Question.Content = question.Content;
                entity.Question.PositivePoints = question.PositivePoints;
                entity.Question.NegativePoints = question.NegativePoints;
                entity.Question.QuestionScoring = question.QuestionScoring;
                entity.Question.IsPublic = question.IsPublic;
                entity.Question.Answers = question.Answers;

                await _dbContext.SaveChangesAsync();
                return entity.Question.Id;
            }
            else
            {
                entity.Question.IsDeleted = true;
                question.Id = 0;
                question.CategoryId = entity.Category.Id;

                return await Add(question);
            }
        }
    }
}
