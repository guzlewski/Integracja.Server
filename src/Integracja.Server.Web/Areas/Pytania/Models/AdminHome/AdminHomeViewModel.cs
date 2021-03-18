using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Alert;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Pytania.Models.AdminHome
{
    public class AdminHomeViewModel
    {
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
        public List<AlertModel> Alerts { get; set; }

        public AdminHomeViewModel() : base()
        {
        }

        public static class Ids
        {
        }
    }
}
