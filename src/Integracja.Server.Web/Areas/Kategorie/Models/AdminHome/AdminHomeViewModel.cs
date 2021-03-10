using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Kategorie.Models.AdminHome
{
    public class AdminHomeViewModel
    {
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public CategoryFormViewModel CategoryFormModel { get; set; } = new CategoryFormViewModel();

        public AdminHomeViewModel() : base()
        {
        }        
    }
}
