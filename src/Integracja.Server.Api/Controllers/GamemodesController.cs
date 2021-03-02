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
    public class GamemodesController : DefaultController
    {
        private readonly IGamemodeService _gamemodeService;

        public GamemodesController(IGamemodeService gamemodeService)
        {
            _gamemodeService = gamemodeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GamemodeGetAll>> GetAll()
        {
            return await _gamemodeService.GetAll(UserId.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GamemodeGet>> Get(int id)
        {
            return await _gamemodeService.Get(id, UserId.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(GamemodeAdd dto)
        {
            var entityId = await _gamemodeService.Add(dto, UserId.Value);
            return Created($"{Request.Path}/{entityId}", null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] GamemodeModify dto)
        {
            var entityId = await _gamemodeService.Update(id, dto, UserId.Value);

            if (entityId != id)
            {
                return Created($"{Request.Path}/{entityId}", null);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _gamemodeService.Delete(id, UserId.Value);
            return NoContent();
        }
    }
}
