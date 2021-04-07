using Integracja.Server.Web.Models.Shared.Alert;
using static Integracja.Server.Web.Models.Shared.Alert.AlertType;
namespace Integracja.Server.Web.Models.Shared.Question
{
    public class QuestionAlert : AlertModel
    {
        public QuestionAlert(AlertType type, string message) : base(type, message)
        {
        }

        public static AlertModel CreateSuccess() => CreateSuccess("pytanie");
        public static AlertModel UpdateSuccess() => UpdateSuccess("pytanie");
        public static AlertModel DeleteSuccess() => DeleteSuccess("pytanie");
        public static AlertModel CreateFailure() => CreateFailure("pytania");
        public static AlertModel UpdateFailure() => UpdateFailure("pytania");
        public static AlertModel DeleteFailure() => DeleteFailure("pytania");
    }
}
