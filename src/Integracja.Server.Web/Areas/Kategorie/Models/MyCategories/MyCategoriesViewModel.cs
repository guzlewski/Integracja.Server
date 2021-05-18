using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Category;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Kategorie.Models.MyCategories
{
    public class MyCategoriesViewModel
    {
        public List<CategoryModel> Categories { get; set; }
        public CategoryFormViewModel Form { get; set; } = new();
        public List<AlertModel> Alerts { get; set; }
    }
}
