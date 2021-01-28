using System;
using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("[action]")]
        public IActionResult Login(LoginDto login)
        {
            throw new NotImplementedException();
        }
    }
}
