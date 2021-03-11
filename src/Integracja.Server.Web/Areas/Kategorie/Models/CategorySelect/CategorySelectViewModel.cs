using Integracja.Server.Web.Models.Shared.Category;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategorySelect
{
    public class CategorySelectViewModel
    { 
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();

        public CategorySelectViewModel() : base()
        {
        }
    }
}
