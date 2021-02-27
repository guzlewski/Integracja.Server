using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Kategorie
{
    public class KategorieViewModel : PageModel
    {
        public List<CategoryGetAll> Categories { get; set; }
    }
}
