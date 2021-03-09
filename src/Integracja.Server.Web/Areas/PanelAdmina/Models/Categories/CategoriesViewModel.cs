using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Categories
{
    public class CategoriesViewModel : CategoryFormViewModel
    {
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public CategoriesViewModel() : base()
        {
        }        
    }
}
