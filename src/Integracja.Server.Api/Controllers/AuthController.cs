using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Integracja.Server.Api.Attributes;
using Integracja.Server.Api.Models;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Integracja.Server.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Api.Controllers
{
    [Mobile]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : DefaultController
    {
        private readonly IAuthService _authService;
        private readonly DefaultSettings _defaultSettings;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthController(IAuthService authService, IOptions<DefaultSettings> options, UserManager<User> userManager, IEmailSender emailSender)
        {
            _authService = authService;
            _userManager = userManager;
            _defaultSettings = options.Value;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Logs user into the system
        /// </summary>
        /// <param name="dto"></param>
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

        /// <summary>
        /// Registers user into the system
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid body supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                ProfilePicture = _defaultSettings.ProfilePicture,
                ProfileThumbnail = _defaultSettings.ProfileThumbnail
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.ActionLink(nameof(ConfirmEmail), null, new { userId = user.Id, code }, Request.Scheme, Request.Host.Value);

                await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Confirms user's email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <response code="204">Successful operation</response>
        /// <response code="400">Invalid body supplied</response>
        /// <response code="404">User with supplied userId not found</response>
        /// <response code="409">Invalid code supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> ConfirmEmail([Required] string userId, [Required] string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException)
            {
                return Conflict();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
    }
}
