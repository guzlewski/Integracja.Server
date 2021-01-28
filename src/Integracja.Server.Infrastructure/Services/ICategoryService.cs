using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> Get(int id);
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<CategoryDto> Create(CategoryDto dto);
        Task Update(int id, CategoryDto dto);
        Task Delete(int id);
    }
}
