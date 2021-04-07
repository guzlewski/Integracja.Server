using Integracja.Server.Web.Areas.Historia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Historia.Controllers
{
    [Area("Historia")]
    public class HistoryQuestionController : Controller
    {
        public IActionResult Index(int questionId)
        {
            HistoryQuestionViewModel Model = new HistoryQuestionViewModel();
            Model.questionId = questionId;

            return View("HistoryQuestion", Model);
        }
    }
}
