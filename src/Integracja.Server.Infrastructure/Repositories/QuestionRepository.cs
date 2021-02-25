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
                    (q.IsPublic || q.OwnerId == userId) &&
                    !q.IsDeleted &&
                    (q.Category.IsPublic || q.Category.OwnerId == userId) &&
                    !q.Category.IsDeleted);
        }

        public IQueryable<Question> GetAll(int userId)
        {
            return _dbContext.Questions
                .AsNoTracking()
                .Where(q => (q.IsPublic || q.OwnerId == userId) &&
                    !q.IsDeleted &&
                    (q.Category.IsPublic || q.Category.OwnerId == userId) &&
                    !q.Category.IsDeleted);
        }

        public async Task<int> Add(Question question)
        {
            var categoryOwnerId = await _dbContext.Categories
                .Where(c => c.Id == question.CategoryId && !c.IsDeleted)
                .Select(c => c.OwnerId)
                .FirstOrDefaultAsync();

            if (categoryOwnerId == default)
            {
                throw new NotFoundException();
            }

            if (categoryOwnerId != question.OwnerId)
            {
                throw new ForbiddenException();
            }

            await _dbContext.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            return question.Id;
        }

        public async Task Delete(Question question)
        {
            var questionEntity = await _dbContext.Questions
                .FirstOrDefaultAsync(q => q.Id == question.Id &&
                    q.OwnerId == question.OwnerId &&
                    !q.IsDeleted &&
                    !q.Category.IsDeleted);

            if (questionEntity == null)
            {
                throw new NotFoundException();
            }

            questionEntity.IsDeleted = true;
            questionEntity.RowVersion++;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Question question)
        {
            var entity = await _dbContext.Questions
                .Include(q => q.Answers)
                .Where(q => q.Id == question.Id &&
                    q.OwnerId == question.OwnerId &&
                    !q.IsDeleted &&
                    !q.Category.IsDeleted)
                .Select(q => new
                {
                    Question = q,
                    GamesCount = q.GameQuestions.Count
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Question.RowVersion++;

            if (entity.GamesCount == 0)
            {
                UpdateQuestion(entity.Question, question);

                await _dbContext.SaveChangesAsync();
                return entity.Question.Id;
            }
            else
            {
                question.Id = 0;
                question.CategoryId = entity.Question.CategoryId;
                entity.Question.IsDeleted = true;

                return await Add(question);
            }
        }

        private static void UpdateQuestion(Question orginal, Question modified)
        {
            orginal.Content = modified.Content;
            orginal.PositivePoints = modified.PositivePoints;
            orginal.NegativePoints = modified.NegativePoints;
            orginal.QuestionScoring = modified.QuestionScoring;
            orginal.IsPublic = modified.IsPublic;

            while (modified.Answers.Count > orginal.Answers.Count)
            {
                orginal.Answers.Add(new Answer());
            }

            if (modified.Answers.Count < orginal.Answers.Count)
            {
                orginal.Answers = orginal.Answers.Take(modified.Answers.Count).ToList();
            }

            using var orginalEnumerator = orginal.Answers.GetEnumerator();
            using var modifiedEnumerator = modified.Answers.GetEnumerator();

            while (orginalEnumerator.MoveNext() && modifiedEnumerator.MoveNext())
            {
                orginalEnumerator.Current.IsCorrect = modifiedEnumerator.Current.IsCorrect;
                orginalEnumerator.Current.Content = modifiedEnumerator.Current.Content;
            }
        }
    }
}
