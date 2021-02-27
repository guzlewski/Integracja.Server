using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models
{
    public class PanelAdminaViewModel : PageModel
    {
        public bool ShowCategories { get; set; }
        public bool ShowQuestions { get; set; }

        public IEnumerable<QuestionGetAll> Questions { get; set; }
        public IEnumerable<User> Users { get; set; }

        public PanelAdminaViewModel() : base()
        {
        }
    }
}
