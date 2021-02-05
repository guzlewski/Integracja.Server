using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface ICategoryService
    {
        Task<CategoryGet> Get(int id, int userId);
        Task<IEnumerable<CategoryGetAll>> GetAll(int userId);
        Task<int> Add(CategoryAdd dto, int userId);        
        Task Delete(int id, int userId);
        Task<int> Update(int id, CategoryModify dto, int userId);
    }
}
