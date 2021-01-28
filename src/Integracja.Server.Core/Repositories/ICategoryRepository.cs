using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> Get(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> Add(Category category);
        Task Update(Category category);
        Task Delete(Category category);
    }
}
