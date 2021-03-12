using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public interface IGameQuestionFormActions
    {
        // GameQuestionCreate wprowadza pytanie do QuestionPool w jakimś cache`u, data store, cokolwiek
        // dlatego QuestionPoolCreate nie przyjmuje już żadnego argumentu, może najwyżej puścić QuestionPool gdzieś dalej
        Task<IActionResult> QuestionPoolCreate();
        Task<IActionResult> GameQuestionCreate(int? id);
        Task<IActionResult> GameQuestionDelete(int? id);
    }
}
