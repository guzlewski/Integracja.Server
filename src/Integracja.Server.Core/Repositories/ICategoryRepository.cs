using System.Collections.Generic;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    interface ICategoryRepository
    {
        Question Get(int id);
        IEnumerable<Question> GetAll();
        Question Add(Question note);
        void Update(Question note);
        void Delete(Question note);
    }
}
