using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models
{
    public class PanelAdminaViewModel : PageModel
    {
        public bool ShowCategories { get; set; }
        public bool ShowQuestions { get; set; }

        public List<CategoryGetAll> Categories { get; set; }
        public List<QuestionGetAll> Questions { get; set; }
    }
}
