using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Get(int id, int userId);
        IQueryable<Category> GetAll(int userId);
        Task<Category> Add(Category category);
        Task Delete(Category category);
        Task Update(Category category);
    }
}
