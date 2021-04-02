using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Integracja.Server.Api.Attributes;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : DefaultController
    {
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private readonly IGamemodeService _gamemodeService;
        private readonly IGameUserService _gameUserService;
        private readonly IPictureService _pictureService;

        public UsersController(ICategoryService categoryService, IQuestionService questionService, IGamemodeService gamemodeService, IGameUserService gameUserService, IPictureService pictureService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
            _gamemodeService = gamemodeService;
            _gameUserService = gameUserService;
            _pictureService = pictureService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryDto>> CreatedCategories()
        {
            return await _categoryService.GetOwned<CategoryDto>(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<QuestionDto>> CreatedQuestions()
        {
            return await _questionService.GetOwned<QuestionDto>(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GamemodeDto>> CreatedGamemodes()
        {
            return await _gamemodeService.GetOwned<GamemodeDto>(UserId.Value);
        }

        /// <summary>
        /// Returns games that user joined and are upcoming or ready to play
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(IEnumerable<GameUserDto<GameDto>>), StatusCodes.Status200OK)]
        [Mobile]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GameUserDto<GameDto>>> Games()
        {
            return await _gameUserService.GetActive<GameUserDto<GameDto>>(UserId.Value);
        }

        /// <summary>
        /// Returns games that user joined and can't play
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(IEnumerable<GameUserDto<GameDto>>), StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GameUserDto<GameDto>>> GamesArchived()
        {
            return await _gameUserService.GetArchived<GameUserDto<GameDto>>(UserId.Value);
        }

        /// <summary>
        /// Returns game that user joined by id 
        /// </summary>
        /// <param name="id">Id of the game</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid id supplied</response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(GameUserDto<DetailGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("[action]/{id}")]
        public async Task<GameUserDto<DetailGameDto>> Games(int id)
        {
            return await _gameUserService.Get<GameUserDto<DetailGameDto>>(id, UserId.Value);
        }

        /// <summary>
        /// Updates user's profile picture and profile thumbnail, returns url to updated picture
        /// </summary>
        /// <param name="formFile"></param>
        /// <response code="201">Successful operation</response>
        /// <response code="400">No picture supplied</response>
        /// <response code="413">Picture too large</response>
        /// <response code="415">Unsupported media type
        /// <para>Supported MIME types:</para>
        /// <para>image/bmp</para>
        /// <para>image/x-windows-bmp</para>
        /// <para>image/gif</para>
        /// <para>image/jpeg</para>
        /// <para>image/pjpeg</para>
        /// <para>image/png</para>
        /// <para>image/tga</para>
        /// <para>image/x-tga</para>
        /// <para>image/x-targa</para>
        /// <para>Supported file extensions:</para>
        /// <para>.bm</para>
        /// <para>.bmp</para>
        /// <para>.dip</para>
        /// <para>.gif</para>
        /// <para>.jpg</para>
        /// <para>.jpeg</para>
        /// <para>.jfif</para>
        /// <para>.png</para>
        /// <para>.tga</para>
        /// <para>.vda</para>
        /// <para>.icb</para>
        /// <para>.vst</para>
        /// </response>
        /// <response code="422">Invalid picture supplied</response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        [HttpPost("[action]")]
        public async Task<IActionResult> ProfilePicture([Required] IFormFile formFile)
        {
            var uri = await _pictureService.Save(formFile, UserId.Value);
            return Created(uri, null);
        }

        /// <summary>
        /// Deletes user's profile picture and profile thumbnail
        /// </summary>
        /// <response code="204">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("[action]")]
        public async Task<IActionResult> ProfilePicture()
        {
            await _pictureService.Delete(UserId.Value);
            return NoContent();
        }
    }
}
