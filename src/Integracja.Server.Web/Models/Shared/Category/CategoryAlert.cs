using Integracja.Server.Web.Models.Shared.Alert;
using static Integracja.Server.Web.Models.Shared.Alert.AlertType;
namespace Integracja.Server.Web.Models.Shared.Category
{
    public class CategoryAlert : AlertModel
    {
        public CategoryAlert(AlertType type, string message) : base(type, message)
        {
        }

        public static AlertModel CreateSuccess() => CreateSuccess("kategorię");
        public static AlertModel UpdateSuccess() => UpdateSuccess("kategorię");
        public static AlertModel DeleteSuccess() => DeleteSuccess("kategorię");
        public static AlertModel CreateFailure() => CreateFailure("kategorii");
        public static AlertModel UpdateFailure() => UpdateFailure("kategorii");
        public static AlertModel DeleteFailure() => DeleteFailure("kategorii");
    }
}
