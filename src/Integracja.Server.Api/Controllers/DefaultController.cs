using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public int? UserId { get; }

        public DefaultController()
        {
            if (int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id))
            {
                UserId = id;
            }
        }
    }
}
