﻿using System.Linq;
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

        public IQueryable<Category> Get(int id)
        {
            return _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.Id == id);
        }

        public IQueryable<Category> GetAll()
        {
            return _dbContext.Categories
                .AsNoTracking();
        }

        public async Task<int> Add(Category category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category.Id;
        }

        public async Task Delete(Category category, bool skipUserVerification = false)
        {
            var categoryEntity = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id &&
                    (c.OwnerId == category.OwnerId || skipUserVerification) &&
                    !c.IsDeleted);

            if (categoryEntity == null)
            {
                throw new NotFoundException();
            }

            categoryEntity.IsDeleted = true;
            categoryEntity.RowVersion++;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Category category, bool skipUserVerification = false)
        {
            var categoryEntity = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id &&
                (c.OwnerId == category.OwnerId || skipUserVerification) &&
                !c.IsDeleted);

            if (categoryEntity == null)
            {
                throw new NotFoundException();
            }

            categoryEntity.RowVersion++;
            UpdateCategory(categoryEntity, category);

            await _dbContext.SaveChangesAsync();
            return categoryEntity.Id;
        }

        private static void UpdateCategory(Category orginal, Category modified)
        {
            orginal.Name = modified.Name;
            orginal.IsPublic = modified.IsPublic;
        }
    }
}
