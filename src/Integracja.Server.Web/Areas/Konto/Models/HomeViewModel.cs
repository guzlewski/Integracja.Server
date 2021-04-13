using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Areas.Konto.Models
{
    public class HomeViewModel : PageModel
    {
        public KontoModel Details { get; set; }

        public HomeViewModel() : base()
        {
            Details = new KontoModel();
        }
    }

}
