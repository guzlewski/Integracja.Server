using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Controllers
{
    public class StworzGreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
