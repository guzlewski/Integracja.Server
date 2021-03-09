using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Pytania.Models.CategorySelect
{
    public class CategorySelectViewModel : CategoryFormViewModel
    { 
        public List<CategoryModel> Categories { get; set; }

        public CategorySelectViewModel() : base()
        {
            Categories = new List<CategoryModel>();
        }

    }
}
