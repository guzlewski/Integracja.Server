using static Integracja.Server.Web.Models.Shared.Alert.AlertType;
namespace Integracja.Server.Web.Models.Shared.Alert
{
    public class AlertModel
    {
        public AlertType Type { get; set; }
        public string Message { get; set; }
        public AlertModel(AlertType type, string message) => (Type, Message) = (type, message);
        public string GetBootstrapClass()
        {
            switch (Type)
            {
                case Success:
                    return "alert-success";
                case Info:
                    return "alert-info";
                case Warning:
                    return "alert-warning";
                case Danger:
                    return "alert-danger";
                case None:
                default:
                    return "";
            }
        }

        public static AlertModel CreateSuccess(string objectName) => new AlertModel
            (Success, "Pomyślnie utworzono " + objectName);
        public static AlertModel UpdateSuccess(string objectName) => new AlertModel
            (Success, "Pomyślnie zaaktualizowano " + objectName);
        public static AlertModel DeleteSuccess(string objectName) => new AlertModel
            (Success, "Pomyślnie usunięto " + objectName);
        public static AlertModel CreateFailure(string objectName) => new AlertModel
            (Danger, "Nie udało się utworzyć " + objectName);
        public static AlertModel UpdateFailure(string objectName) => new AlertModel
            (Danger, "Nie udało się zaaktualizować " + objectName);
        public static AlertModel DeleteFailure(string objectName) => new AlertModel
            (Danger, "Nie udało się usunąć " + objectName);
    }
}
