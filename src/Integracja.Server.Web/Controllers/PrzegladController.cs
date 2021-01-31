using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Controllerss
{
    public class PrzegladController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
