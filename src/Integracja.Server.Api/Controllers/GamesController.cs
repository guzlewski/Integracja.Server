using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Api.Utilities;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
﻿using System;
using Integracja.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : DefaultController
    {
        private readonly IGameService _gameService;
        private readonly IGameUserService _gameUserService;
        private readonly IGameLogicService _gameQuestionService;

        public GamesController(IGameService gameService, IGameUserService gameUserService, IGameLogicService gameQuestionService)
        {
            _gameService = gameService;
            _gameUserService = gameUserService;
            _gameQuestionService = gameQuestionService;
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

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpGet("[action]/{guid}")]
        public async Task<IActionResult> Join(Guid guid)
        {
            var entityId = await _gameUserService.Join(guid, UserId.Value);
            return CreatedAtAction(nameof(UsersController.Games), new { controller = ControllerHelper.GetName<UsersController>(), id = entityId }, null);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Leave(int id)
        {
            await _gameUserService.Leave(id, UserId.Value);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpGet("[action]/{id}")]
        public async Task<GameQuestionDto> Play(int id)
        {
            return await _gameQuestionService.GetQuestion<GameQuestionDto>(id, UserId.Value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("[action]/{gameId}/{questionId}")]
        public async Task<GameUserQuestionDto> Play(int gameId, int questionId, IEnumerable<int> answers)
        {
            return await _gameQuestionService.SaveAnswers<GameUserQuestionDto>(gameId, UserId.Value, questionId, answers);
        }
    }
}
