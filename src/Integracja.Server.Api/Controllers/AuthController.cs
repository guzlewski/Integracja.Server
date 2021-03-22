using System.Threading.Tasks;
using Integracja.Server.Api.Attributes;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Mobile]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : DefaultController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Logs user into the system
        /// </summary>
        /// <param name="dto">gdfgdf</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid body supplied</response>
        /// <response code="401">Invalid username or password supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(DetailUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<DetailUserDto> Login(LoginDto dto)
        {
            return await _authService.Login(dto);
        }

        /// <summary>
        /// Logs out current logged in user session
        /// </summary>
        /// <response code="204">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(UserId.Value);
            return NoContent();
        }
    }
}
