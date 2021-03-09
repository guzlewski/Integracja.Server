using Integracja.Server.Web.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Models.Shared.Category
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

