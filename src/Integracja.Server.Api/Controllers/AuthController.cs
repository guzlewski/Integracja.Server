using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        public async Task<TokenDTO> Login(LoginDto dto)
        {
            return await _tokenService.GenerateToken(dto);
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
