using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.AdminHome
{
    public class AdminHomeViewModel
    {
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
        public QuestionAlert Alert { get; set; } = new QuestionAlert(Web.Models.Shared.Alert.AlertType.None, "");

        public AdminHomeViewModel() : base()
        {
        }

        public static class Ids
        {
        }
    }
}
