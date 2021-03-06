using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Models.Home
{
    public class HomeViewModel : PageModel
    {
        public List<QuestionModel> Questions { get; set; }

        public HomeViewModel() : base()
        {
            Questions = new List<QuestionModel>();
        }

        public static class Ids
        {
        } 
    }
}
