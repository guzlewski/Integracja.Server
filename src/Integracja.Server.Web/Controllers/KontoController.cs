using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Controllers
{
    public class KontoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
