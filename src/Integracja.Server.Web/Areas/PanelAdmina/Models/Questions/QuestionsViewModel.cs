using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Questions
{
    public class QuestionsViewModel
    {
        public List<QuestionGetAll> Questions { get; set; }

        public QuestionsViewModel() : base()
        {
            Questions = new List<QuestionGetAll>();
        }

        public static class Ids
        {
        }
    }
}
