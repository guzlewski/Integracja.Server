using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Api.Utilities;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
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

        public GamesController(IGameService gameService, IGameUserService gameUserService)
        {
            _gameService = gameService;
            _gameUserService = gameUserService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GameDto>> GetAll()
        {
            return await _gameService.GetAll(UserId.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DetailGameDto> Get(int id)
        {
            return await _gameService.Get(id, UserId.Value);
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
    }
}
