using Integracja.Server.Web.Models.Shared.Question;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Historia.Models
{
    public class HistoryQuestionViewModel
    {
        public QuestionModel question { get; set; }

        public List<guser> usersStats { get; set; }
    }

    public class guser
    {
        public string username;
        public int questionScore;
        public List<int> answersState;
    }
}
