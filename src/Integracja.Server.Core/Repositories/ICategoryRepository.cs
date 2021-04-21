using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Get(int id);
        IQueryable<Category> GetAll();
        Task<int> Add(Category category);
        Task Delete(Category category, bool skipUserVerification = false);
        Task<int> Update(Category category, bool skipUserVerification = false);
    }
}
