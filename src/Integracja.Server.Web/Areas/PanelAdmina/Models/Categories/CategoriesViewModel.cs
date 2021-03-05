using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Categories
{
    public class CategoriesViewModel : PageModel
    {
        public List<CategoryGetAll> Categories { get; set; }

        public CategoryModel Category { get; set; }

        public CategoriesViewModel() : base()
        {
            Categories = new List<CategoryGetAll>();
            Category = new CategoryModel();
        }        
    }
}
