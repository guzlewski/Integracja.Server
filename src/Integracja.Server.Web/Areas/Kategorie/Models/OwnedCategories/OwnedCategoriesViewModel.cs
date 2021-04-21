using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Integracja.Server.Web.Models.Shared.Category;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Kategorie.Models.OwnedCategories
{
    public class OwnedCategoriesViewModel
    {
        public List<CategoryModel> Categories { get; set; }
        public CategoryFormViewModel Form { get; set; } = new();
    }
}
