using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Questions
{
    public class QuestionsViewModel
    {
        public List<QuestionDto> Questions { get; set; }
        public QuestionAlert Alert { get; set; }

        public QuestionsViewModel() : base()
        {
            Questions = new List<QuestionDto>();
        }

        public static class Ids
        {
        }
    }
}
