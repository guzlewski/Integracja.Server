using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.PanelAdmina
{
    public class PanelAdminaViewModel : PageModel
    {
        public bool ShowCategories { get; set; }
        public bool ShowQuestions { get; set; }

        public IEnumerable<CategoryGetAll> Categories { get; set; }
        public IEnumerable<QuestionGetAll> Questions { get; set; }

        public PanelAdminaViewModel() : base()
        {
            Categories = new List<CategoryGetAll>();
        }
    }
}
