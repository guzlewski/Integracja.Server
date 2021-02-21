using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : DefaultController
    {
        private readonly IGameService _gameService;
        private readonly IGameUserService _gameUserService;
        private readonly IGameQuestionService _gameQuestionService;

        public GamesController(IGameService gameService, IGameUserService gameUserService, IGameQuestionService gameQuestionService)
        {
            _gameService = gameService;
            _gameUserService = gameUserService;
            _gameQuestionService = gameQuestionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GameGetAll>> GetAll()
        {
            return await _gameService.GetAll(UserId.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameGet>> Get(int id)
        {
            return await _gameService.Get(id, UserId.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(GameAdd dto)
        {
            var entityId = await _gameService.Add(dto, UserId.Value);
            return Created($"{Request.Path}/{entityId}", null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] GameModify dto)
        {
            await _gameService.Update(id, dto, UserId.Value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _gameService.Delete(id, UserId.Value);
            return NoContent();
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Accept(int id)
        {
            await _gameUserService.Accept(id, UserId.Value);
            return NoContent();
        }

        [HttpGet("[action]/{guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Join(Guid guid)
        {
            var entityId = await _gameUserService.Join(guid, UserId.Value);
            return Created($"{Request.Path}/{entityId}", null);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Leave(int id)
        {
            await _gameUserService.Leave(id, UserId.Value);
            return NoContent();
        }

        [HttpGet("MyGames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GameUserGetAll>> GetMyGames()
        {
            return await _gameUserService.GetAll(UserId.Value);
        }

        [HttpGet("MyGames/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameUserGet>> GetMyGames(int id)
        {
            return await _gameUserService.Get(id, UserId.Value);
        }

        [HttpGet("Play/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GameQuestionGet> Play(int id)
        {
            return await _gameQuestionService.GetQuestion(id, UserId.Value);
        }

        [HttpPost("Play/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<GameUserQuestionGet> Play(ICollection<int> answers, int id)
        {
            return await _gameQuestionService.SaveAnswers(id, UserId.Value, answers);
        }
    }
}
