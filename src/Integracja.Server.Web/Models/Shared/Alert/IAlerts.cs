using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Shared.Alert
{
    public interface IAlerts
    {
        void SetAlerts(List<AlertModel> alerts);
        List<AlertModel> GetAlerts();
    }
}
