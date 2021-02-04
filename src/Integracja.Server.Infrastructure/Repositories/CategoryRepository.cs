using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Category> Get(int id, int userId)
        {
            var entity = _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.Id == id && (c.IsPublic || c.OwnerId == userId) && !c.IsDeleted);

            return entity;
        }

        public IQueryable<Category> GetAll(int userId)
        {
            var entities = _dbContext.Categories
                .AsNoTracking()
                .Where(c => (c.IsPublic || c.OwnerId == userId) && !c.IsDeleted);

            return entities;
        }

        public async Task<Category> Add(Category category)
        {
            category.Id = 0;

            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task Delete(Category category)
        {
            var entity = await _dbContext.Categories
                .Where(c => c.Id == category.Id && c.OwnerId == category.OwnerId && !c.IsDeleted)
                .Select(c => new
                {
                    Category = c,
                    QuestionsCount = c.Questions.Count
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.QuestionsCount == 0)
            {
                _dbContext.Remove(entity.Category);
            }
            else
            {
                entity.Category.IsDeleted = true;
                entity.Category.RowVersion++;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            var entity = await _dbContext.Categories
               .FirstOrDefaultAsync(c => c.Id == category.Id && c.OwnerId == category.OwnerId && !c.IsDeleted);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Name = category.Name;
            entity.IsPublic = category.IsPublic;
            entity.RowVersion++;

            await _dbContext.SaveChangesAsync();
        }
    }
}
