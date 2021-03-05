using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.DodajPytania.Models.CategorySelect
{
    public class CategorySelectViewModel : PageModel
    { 
        public List<CategoryModel> Categories { get; set; }
        public CategoryModel Category { get; set; }

        public CategorySelectViewModel()
        {
            Categories = new List<CategoryModel>();
            Category = new CategoryModel();
        }

    }
}
