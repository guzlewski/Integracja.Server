using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<T> Get<T>(int id, int userId);
        Task<IEnumerable<T>> GetAll<T>(int userId);
        Task<IEnumerable<T>> GetOwned<T>(int userId);
        Task<IEnumerable<T>> GetOwned<T>(int categoryId, int userId);
        Task<int> Add(CreateQuestionDto createQuestionDto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, EditQuestionDto editQuestionDto, int userId);
    }
}
