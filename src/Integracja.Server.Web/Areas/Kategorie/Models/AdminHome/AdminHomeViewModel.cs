using System.Collections.Generic;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Integracja.Server.Web.Models.Shared.Alert;

namespace Integracja.Server.Web.Areas.Kategorie.Models.AdminHome
{
    public class AdminHomeViewModel
    {
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public CategoryFormViewModel CategoryFormModel { get; set; } = new CategoryFormViewModel();
        public List<AlertModel> Alerts { get; set; }

        public AdminHomeViewModel() : base()
        {
        }
    }
}
