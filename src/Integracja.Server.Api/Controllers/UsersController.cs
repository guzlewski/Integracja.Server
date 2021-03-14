using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
            return await _categoryService.GetOwned(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<QuestionDto>> CreatedQuestions()
        {
            return await _questionService.GetOwned(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GamemodeDto>> CreatedGamemodes()
        {
            return await _gamemodeService.GetOwned(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GameUserDto>> Games()
        {
            return await _gameUserService.GetActive(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GameUserDto>> GamesArchived()
        {
            return await _gameUserService.GetArchived(UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("[action]/{id}")]
        public async Task<DetailGameUserDto> Games(int id)
        {
            return await _gameUserService.Get(id, UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadPicture([Required] IFormFile profilePicture)
        {
            var uri = await _pictureService.Save(profilePicture, UserId.Value);
            return Created(uri, null);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("[action]")]
        public async Task<IActionResult> DeletePicture()
        {
            await _pictureService.Delete(UserId.Value);
            return NoContent();
        }
    }
}
