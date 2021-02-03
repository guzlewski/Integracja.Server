using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface ICategoryService
    {
        Task<CategoryDetailsDto> Get(int id, int userId);
        Task<IEnumerable<CategoryDto>> GetAll(int userId);
        Task<CategoryDto> Add(CategoryDto dto, int userId);
        Task Update(int id, CategoryDto dto, int userId);
        Task Delete(int id, int userId);
    }
}
