using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<DetailQuestionDto> Get(int id, int userId);
        Task<IEnumerable<QuestionDto>> GetAll(int userId);
        Task<int> Add(CreateQuestionDto createQuestionDto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, EditQuestionDto editQuestionDto, int userId);
    }
}
