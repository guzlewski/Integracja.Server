using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Api.Attributes;
using Integracja.Server.Api.Utilities;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : DefaultController
    {
        private readonly IGameService _gameService;
        private readonly IGameUserService _gameUserService;
        private readonly IGameLogicService _gameLogicService;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gameService, IGameUserService gameUserService, IGameLogicService gameLogicService, ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _gameUserService = gameUserService;
            _gameLogicService = gameLogicService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GameDto>> GetAll()
        {
            return await _gameService.GetAll<GameDto>(UserId.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DetailGameDto> Get(int id)
        {
            return await _gameService.Get<DetailGameDto>(id, UserId.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add(CreateGameDto createGameDto)
        {
            var entityId = await _gameService.Add(createGameDto, UserId.Value);
            return CreatedAtAction(nameof(Get), new { id = entityId }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, [FromBody] EditGameDto editGameDto)
        {
            await _gameService.Update(id, editGameDto, UserId.Value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _gameService.Delete(id, UserId.Value);
            return NoContent();
        }

        /// <summary>
        /// Adds current logged in user to game
        /// </summary>
        /// <param name="guid" example="00000000-0000-0000-0000-000000000000">Guid of the game to join</param>
        /// <response code="201">Successful operation</response>
        /// <response code="400">Invalid guid supplied</response>
        /// <response code="404">Game not found</response>
        /// <response code="409">Couldn't join to game, possible ErrorCodes:
        /// <para>6 - game is full</para>
        /// <para>5 - game has ended</para>
        /// <para>3 - user already joined this game</para>
        /// </response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpGet("[action]/{guid}")]
        public async Task<IActionResult> Join(Guid guid)
        {
            var entityId = await _gameUserService.Join(guid, UserId.Value);
            return CreatedAtAction(nameof(UsersController.Games), new { controller = ControllerHelper.GetName<UsersController>(), id = entityId }, null);
        }

        /// <summary>
        /// Removes current logged in user from game
        /// </summary>
        /// <param name="id">Id of the game to leave</param>
        /// <response code="204">Successful operation</response>
        /// <response code="400">Invalid id supplied</response>
        /// <response code="404">Game not found</response>
        /// <response code="409">Couldn't left the game, possible ErrorCodes:
        /// <para>5 - game has ended</para>
        /// </response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Leave(int id)
        {
            await _gameUserService.Leave(id, UserId.Value);
            return NoContent();
        }


        /// <summary>
        /// Returns first not answered question from the game
        /// </summary>
        /// <param name="id">Id of the game that user is playing</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid id supplied</response>
        /// <response code="404">Game not found</response>
        /// <response code="409">Couldn't get questions, possible ErrorCodes:
        /// <para>4 - game has been cancelled</para>
        /// <para>5 - game has ended</para>
        /// <para>1 - user already answered all questions</para>
        /// <para>7 - game is over due to gamemode rules</para>
        /// <para>8 - game time has expired</para>
        /// <para>10 - game has not started yet</para>
        /// </response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(GameUserQuestionDto<AnswerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpGet("[action]/{id}")]
        public async Task<GameUserQuestionDto<AnswerDto>> Play(int id)
        {
            return await _gameLogicService.GetQuestion<GameUserQuestionDto<AnswerDto>>(id, UserId.Value);
        }

        /// <summary>
        /// Saves user's answers to the question and returns result
        /// </summary>
        /// <param name="gameId">Id of the game that user is playing</param>
        /// <param name="questionId">Id of question that user is answering</param>
        /// <param name="answers">Ids of answers that user selected as correct</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid gameId or questionId or answers supplied</response>
        /// <response code="404">Game or question not found</response>
        /// <response code="409">Couldn't save answers, possible ErrorCodes:
        /// <para>4 - game has been cancelled</para>
        /// <para>5 - game has ended</para>
        /// <para>2 - user already answered to this question</para>
        /// <para>7 - game is over due to gamemode rules</para>
        /// <para>9 - question time has expired</para>
        /// <para>8 - game time has expired</para>
        /// <para>10 - game has not started yet</para>
        /// </response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(GameUserQuestionDto<DetailAnswerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpPost("[action]/{gameId}/{questionId}")]
        public async Task<GameUserQuestionDto<DetailAnswerDto>> Play(int gameId, int questionId, [Required] IEnumerable<int> answers)
        {
            var x = "";

            foreach (var a in answers)
            {
                x += $"{a}, ";
            }

            _logger.LogWarning($"Answers {answers.Count()}, {x}");

            return await _gameLogicService.SaveAnswers<GameUserQuestionDto<DetailAnswerDto>>(gameId, UserId.Value, questionId, answers);
        }

        /// <summary>
        /// Returns history of the game by id
        /// </summary>
        /// <param name="id">Id of the game</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid gameId supplied</response>
        /// <response code="404">Game not found or is still in progress or has been cancelled</response>
        /// <response code="500">Internal server error</response>
        [Mobile]
        [ProducesResponseType(typeof(DetailGameUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("[action]/{id}")]
        public async Task<DetailGameUserDto> History(int id)
        {
            return await _gameLogicService.GetHistory<DetailGameUserDto>(id, UserId.Value);
        }
    }
}
