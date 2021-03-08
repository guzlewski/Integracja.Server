using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Controllers.Przeglad
{
    public class PrzegladController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
