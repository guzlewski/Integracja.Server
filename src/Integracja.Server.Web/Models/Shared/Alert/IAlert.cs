namespace Integracja.Server.Web.Models.Shared.Alert
{
    public interface IAlert
    {
        void SetAlert<T>(T alert) where T:AlertModel;
        T GetAlert<T>() where T:AlertModel;
    }
}
