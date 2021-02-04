using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Get(int id, int userId);
        IQueryable<Question> GetAll(int userId);
        Task<Question> Add(Question question);
        Task Delete(Question question);
        Task Update(Question question);
    }
}
