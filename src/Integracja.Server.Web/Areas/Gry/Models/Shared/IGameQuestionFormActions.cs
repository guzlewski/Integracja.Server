using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Models.Shared
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
