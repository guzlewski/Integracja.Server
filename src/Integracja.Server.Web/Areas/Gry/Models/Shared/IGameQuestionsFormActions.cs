using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Models.Shared
{
    public interface IGameQuestionsFormActions
    {
        Task<IActionResult> GameQuestionsCreate(List<int> gameQuestions);
    }
}
