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

        public async Task<DetailCategoryDto> Get(int id, int userId)
        {
            var detailCategoryDto = await _categoryRepository.Get(id)
                .Where(c => (c.IsPublic || c.OwnerId == userId) && !c.IsDeleted)
                .ProjectTo<DetailCategoryDto>(_configuration, new Dictionary<string, object> { { "userId", userId } })
                .FirstOrDefaultAsync();

            if (detailCategoryDto == null)
            {
                throw new NotFoundException();
            }

            return detailCategoryDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll(int userId)
        {
            return await _categoryRepository.GetAll()
                .Where(c => (c.IsPublic || c.OwnerId == userId) && !c.IsDeleted)
                .ProjectTo<CategoryDto>(_configuration, new Dictionary<string, object> { { "userId", userId } })
                .ToListAsync();
        }


        public async Task<IEnumerable<CategoryDto>> GetOwned(int userId)
        {
            return await _categoryRepository.GetAll()
                .Where(c => c.OwnerId == userId &&
                    !c.IsDeleted)
                .ProjectTo<CategoryDto>(_configuration, new Dictionary<string, object> { { "userId", userId } })
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

        public async Task Delete(int id, int userId)
        {
            await _categoryRepository.Delete(new Category
            {
                Id = id,
                OwnerId = userId
            });
        }

        public async Task<int> Update(int id, EditCategoryDto editCategoryDto, int userId)
        {
            var category = _mapper.Map<Category>(editCategoryDto);
            category.Id = id;
            category.OwnerId = userId;

            return await _categoryRepository.Update(category);
        }
    }
}
