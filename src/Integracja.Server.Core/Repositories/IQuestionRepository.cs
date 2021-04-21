using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Get(int id);
        IQueryable<Question> GetAll();
        Task<int> Add(Question question, bool skipUserVerification = false);
        Task Delete(Question question, bool skipUserVerification = false);
        Task<int> Update(Question question, bool skipUserVerification = false);
    }
}
