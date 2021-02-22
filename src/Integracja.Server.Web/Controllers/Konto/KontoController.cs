using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Controllers.Konto
{
    public class KontoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
