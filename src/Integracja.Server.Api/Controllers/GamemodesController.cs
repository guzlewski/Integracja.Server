﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamemodesController : DefaultController
    {
        private readonly IGamemodeService _gamemodeService;

        public GamemodesController(IGamemodeService gamemodeService)
        {
            _gamemodeService = gamemodeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GamemodeDto>> GetAll()
        {
            return await _gamemodeService.GetAll<GamemodeDto>(UserId.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DetailGamemodeDto> Get(int id)
        {
            return await _gamemodeService.Get<DetailGamemodeDto>(id, UserId.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CreateGamemodeDto createGamemodeDto)
        {
            var entityId = await _gamemodeService.Add(createGamemodeDto, UserId.Value);
            return CreatedAtAction(nameof(Get), new { id = entityId }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EditGamemodeDto editGamemodeDto)
        {
            var entityId = await _gamemodeService.Update(id, editGamemodeDto, UserId.Value);

            if (entityId != id)
            {
                return CreatedAtAction(nameof(Get), new { id = entityId }, null);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _gamemodeService.Delete(id, UserId.Value);
            return NoContent();
        }
    }
}
