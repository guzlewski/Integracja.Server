using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamemodesController : ControllerBase
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
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return await _gamemodeService.GetAll(userId);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GamemodeGet>> Get(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return await _gamemodeService.Get(id, userId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(GamemodeAdd dto)
        {
            var entityId = await _gamemodeService.Add(dto, LoggedUserId());
            return Created($"{Request.Path}/{entityId}", null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] GamemodeModify dto)
        {
            var entityId = await _gamemodeService.Update(id, dto, LoggedUserId());

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
            await _gamemodeService.Delete(id, LoggedUserId());
            return NoContent();
        }

        private int LoggedUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
