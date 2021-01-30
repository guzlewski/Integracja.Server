using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.DTO;

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

        public async Task<CategoryDto> Create(CategoryDto dto, int userId)
        {
            var category = new Category { Name = dto.Name, IsPublic = dto.IsPublic, AuthorId = userId };
            var entity = await _categoryRepository.Add(category);

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task Delete(int id, int userId)
        {
            await _categoryRepository.Delete(new Category { Id = id, AuthorId = userId });
        }

        public async Task<CategoryDto> Get(int id, int userId)
        {
            var entity = await _categoryRepository.Get(id, userId);

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<IEnumerable<CategoryDto>> GetAll(int userId)
        {
            var entities = await _categoryRepository.GetAll(userId);

            return _mapper.Map<IEnumerable<CategoryDto>>(entities);
        }

        public async Task Update(int id, CategoryDto dto, int userId)
        {
            var category = _mapper.Map<Category>(dto);
            category.Id = id;
            category.AuthorId = userId;

            await _categoryRepository.Update(category);
        }
    }
}
