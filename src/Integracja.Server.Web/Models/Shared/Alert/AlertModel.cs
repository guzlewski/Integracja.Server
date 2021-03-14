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
    }
}
