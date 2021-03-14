using Integracja.Server.Web.Models.Shared.Alert;
using static Integracja.Server.Web.Models.Shared.Alert.AlertType;
namespace Integracja.Server.Web.Models.Shared.Question
{
    public class QuestionAlert : AlertModel
    {
        public QuestionAlert(AlertType type, string message) : base(type, message)
        {
        }

        public static QuestionAlert QuestionCreateSuccess() => new QuestionAlert
            (Success, "Pomyślnie utworzono pytanie");
        public static QuestionAlert QuestionUpdateSuccess() => new QuestionAlert
            (Success, "Pomyślnie zaaktualizowano pytanie");
        public static QuestionAlert QuestionDeleteSuccess() => new QuestionAlert
            (Success, "Pomyślnie usunięto pytanie");
        public static QuestionAlert QuestionCreateFailure() => new QuestionAlert
            (Danger, "Nie udało się utworzyć pytania");
        public static QuestionAlert QuestionUpdateFailure() => new QuestionAlert
            (Danger, "Nie udało się zaaktualizować pytania");
        public static QuestionAlert QuestionDeleteFailure() => new QuestionAlert
            (Danger, "Nie udało się usunąć pytania");
    }
}
