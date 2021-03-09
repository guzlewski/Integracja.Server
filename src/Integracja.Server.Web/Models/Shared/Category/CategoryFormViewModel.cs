using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Models.Shared.Category
{
    public class CategoryFormViewModel : PageModel
    {
        public CategoryModel Category { get; set; }

        public CategoryFormViewModel()
        {
            Category = new CategoryModel();
        }

    }
}

