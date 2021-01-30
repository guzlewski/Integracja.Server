using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
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

        public async Task<Category> Add(Category category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task Delete(Category category)
        {
            var entity = _dbContext.Categories
                .FirstOrDefault(c => c.Id == category.Id && c.AuthorId == category.AuthorId);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.QuestionsCount == 0)
            {
                _dbContext.Remove(entity);
            }
            else
            {
                entity.UpdatedDate = DateTimeOffset.Now;
                entity.IsDeleted = true;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> Get(int id, int userId)
        {
            var entity = await _dbContext.Categories
                .AsNoTracking()
                .Include(c => c.Questions)
                .Include(c => c.Author)
                .FirstOrDefaultAsync(c => c.Id == id && (c.IsPublic || c.AuthorId == userId));

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<Category>> GetAll(int userId)
        {
            var entities = await _dbContext.Categories
                .AsNoTracking()
                .Include(c => c.Author)
                .Where(c => c.IsPublic || c.AuthorId == userId)
                .ToListAsync();

            return entities;
        }

        public async Task Update(Category category)
        {
            var entity = await _dbContext.Categories
               .FirstOrDefaultAsync(c => c.Id == category.Id && c.AuthorId == category.AuthorId);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Name = category.Name;
            entity.IsPublic = category.IsPublic;
            entity.UpdatedDate = DateTimeOffset.Now;

            await _dbContext.SaveChangesAsync();
        }
    }
}
