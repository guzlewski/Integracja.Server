using Integracja.Server.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Utilities
{
    public static class ControllerHelper
    {
        public static string GetName<T>() where T : DefaultController
        {
            return typeof(T).Name.Replace(nameof(Controller), string.Empty);
        }
    }
}
