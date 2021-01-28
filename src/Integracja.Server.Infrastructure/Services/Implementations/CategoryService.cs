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

        public async Task<CategoryDto> Create(CategoryDto dto)
        {
            var category = new Category(dto.Name, dto.IsPublic, dto.AuthorId);
            var entity = await _categoryRepository.Add(category);

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task Delete(int id)
        {
            await _categoryRepository.Delete(new Category { Id = id });
        }

        public async Task<CategoryDto> Get(int id)
        {
            var entity = await _categoryRepository.Get(id);

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var entities = await _categoryRepository.GetAll();

            return _mapper.Map<IEnumerable<CategoryDto>>(entities);
        }

        public async Task Update(int id, CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            category.Id = id;

            await _categoryRepository.Update(category);
        }
    }
}
