using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> Add(Category category);
        Task Delete(Category category);
        Task<Category> Get(int id, int userId);
        Task<IEnumerable<Category>> GetAll(int userId);
        Task Update(Category category);
    }
}
