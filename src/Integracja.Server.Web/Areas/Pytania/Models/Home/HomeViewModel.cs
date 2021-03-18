using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Pytania.Models.Home
{
    public class HomeViewModel : PageModel
    {
        public List<QuestionModel> Questions { get; set; }
        public List<AlertModel> Alerts { get; set; }

        public HomeViewModel() : base()
        {
            Questions = new List<QuestionModel>();
        }

        public static class Ids
        {
        } 
    }
}
