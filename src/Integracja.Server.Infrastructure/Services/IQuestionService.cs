using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IQuestionService
    {
        Task<QuestionGet> Get(int id, int userId);
        Task<IEnumerable<QuestionGetAll>> GetAll(int userId);
        Task<int> Add(QuestionAdd dto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, QuestionModify dto, int userId);
    }
}
