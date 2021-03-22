using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<T> Get<T>(int id, int userId);
        Task<IEnumerable<T>> GetAll<T>(int userId);
        Task<IEnumerable<T>> GetOwned<T>(int userId);
        Task<int> Add(CreateCategoryDto createCategoryDto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, EditCategoryDto editCategoryDto, int userId);
    }
}
