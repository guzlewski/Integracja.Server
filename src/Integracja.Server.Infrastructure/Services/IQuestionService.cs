using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IQuestionService
    {
        Task<QuestionDetailsDto> Get(int id, int userId);
        Task<IEnumerable<QuestionDto>> GetAll(int userId);
        Task<QuestionDetailsDto> Add(QuestionDetailsDto dto, int userId);
        Task Delete(int id, int userId);
        Task Update(int id, QuestionDetailsDto dto, int userId);
    }
}
