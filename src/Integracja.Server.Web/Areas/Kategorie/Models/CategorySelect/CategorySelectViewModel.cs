using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategorySelect
{
    public class CategorySelectViewModel
    { 
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();

        public CategoryFormViewModel CategoryFormModel { get; set; } = new CategoryFormViewModel();

        public CategorySelectViewModel() : base()
        {
        }
    }
}
