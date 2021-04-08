using System.Collections.Generic;
using Integracja.Server.Web.Models.Shared.Category;

namespace Integracja.Server.Web.Areas.Kategorie.Models.Shared
{
    public class CategorySelectViewModel
    {
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public int? CategorySelected { get; set; }

        public CategorySelectViewModel() : base()
        {
        }
    }
}
