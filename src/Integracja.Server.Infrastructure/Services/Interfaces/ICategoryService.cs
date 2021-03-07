using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public interface ICategoryService
    {
        Task<DetailCategoryDto> Get(int id, int userId);
        Task<IEnumerable<CategoryDto>> GetAll(int userId);
        Task<int> Add(CreateCategoryDto createCategoryDto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, EditCategoryDto editCategoryDto, int userId);
    }
}
