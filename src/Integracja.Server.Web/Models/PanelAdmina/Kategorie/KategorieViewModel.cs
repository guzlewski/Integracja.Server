using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.PanelAdmina.Kategorie
{
    public class KategorieViewModel : PageModel
    {
        public List<CategoryGetAll> Categories { get; set; }
    }
}
