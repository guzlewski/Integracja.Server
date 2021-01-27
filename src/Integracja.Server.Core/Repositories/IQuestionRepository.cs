using System.Collections.Generic;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IQuestionRepository
    {
        Question Get(int id);
        IEnumerable<Question> GetAll();
        Question Add(Question note);
        void Update(Question note);
        void Delete(Question note);
    }
}
