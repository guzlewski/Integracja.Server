using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IConfigurationProvider configuration)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<T> Get<T>(int id, int userId, bool skipUserVerification = false)
        {
            var dto = await _categoryRepository.Get(id)
                .Where(c => (c.IsPublic || c.OwnerId == userId || skipUserVerification) && !c.IsDeleted)
                .ProjectTo<T>(_configuration, new Dictionary<string, object> { { "userId", userId } })
                .FirstOrDefaultAsync();

            if (dto == null)
            {
                throw new NotFoundException();
            }

            return dto;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int userId, bool skipUserVerification = false)
        {
            return await _categoryRepository.GetAll()
                .Where(c => (c.IsPublic || c.OwnerId == userId || skipUserVerification) && !c.IsDeleted)
                .ProjectTo<T>(_configuration, new Dictionary<string, object> { { "userId", userId } })
                .ToListAsync();
        }


        public async Task<IEnumerable<T>> GetOwned<T>(int userId)
        {
            return await _categoryRepository.GetAll()
                .Where(c => c.OwnerId == userId && !c.IsDeleted)
                .ProjectTo<T>(_configuration, new Dictionary<string, object> { { "userId", userId } })
                .ToListAsync();
        }

        public async Task<int> Add(CreateCategoryDto createCategoryDto, int userId)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            category.OwnerId = userId;

            foreach (var question in category.Questions)
            {
                question.OwnerId = userId;
            }

            return await _categoryRepository.Add(category);
        }

        public async Task Delete(int id, int userId, bool skipUserVerification = false)
        {
            await _categoryRepository.Delete(new Category
            {
                Id = id,
                OwnerId = userId
            }, skipUserVerification);
        }

        public async Task<int> Update(int id, EditCategoryDto editCategoryDto, int userId, bool skipUserVerification = false)
        {
            var category = _mapper.Map<Category>(editCategoryDto);
            category.Id = id;
            category.OwnerId = userId;

            return await _categoryRepository.Update(category, skipUserVerification);
        }
    }
}
