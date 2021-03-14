using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Konto.Models
{
    public class HomeViewModel : PageModel
    {
        public KontoModel NewFile { get; set; }

        public HomeViewModel() : base()
        {
            NewFile = new KontoModel();
        }
    }

}
