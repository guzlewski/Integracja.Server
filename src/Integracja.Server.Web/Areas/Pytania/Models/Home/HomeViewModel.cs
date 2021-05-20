using System.Collections.Generic;
using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Areas.Pytania.Models.Home
{
    public class HomeViewModel : PageModel
    {
        public string Title = "Pytania";
        public QuestionTableViewModel QuestionTable { get; set; }
        public List<AlertModel> Alerts { get; set; }

        public HomeViewModel() : base()
        {
            QuestionTable = new QuestionTableViewModel();
        }
    }
}
