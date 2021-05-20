using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public class QuestionDetailsViewModel
    {
        public QuestionModel Question { get; set; }
        public string GoBackActionName { get; set; } = ""; // asp-action="" i domyślnie powinno iść do Index kontrolera
        public bool ReadActionsOnly { get; set; } = false;
    }
}
