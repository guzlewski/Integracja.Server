using Integracja.Server.Web.Models.Shared.Alert;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameAlert : AlertModel
    {
        public GameAlert(AlertType type, string message) : base(type, message)
        {
        }
        public static AlertModel CreateSuccess() => CreateSuccess("grę");
        public static AlertModel UpdateSuccess() => UpdateSuccess("grę");
        public static AlertModel DeleteSuccess() => DeleteSuccess("grę");
        public static AlertModel CreateFailure() => CreateFailure("gry");
        public static AlertModel UpdateFailure() => UpdateFailure("gry");
        public static AlertModel DeleteFailure() => DeleteFailure("gry");
    }
}
