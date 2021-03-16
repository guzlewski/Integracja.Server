using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Areas.Kategorie.Models.Shared
{
    public class CategoryFormViewModel : PageModel
    {
        public CategoryModel Category { get; set; } = new CategoryModel();
        public ViewMode ViewMode { get; set; } = ViewMode.Creating;

        public CategoryFormViewModel()
        {
        }

        public CategoryFormViewModel(ViewMode viewMode) => (ViewMode) = (viewMode);
    }
}

