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
    public class CategoriesController : DefaultController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CategoryGetAll>> GetAll()
        {
            return await _categoryService.GetAll(UserId.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryGet>> Get(int id)
        {
            return await _categoryService.Get(id, UserId.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(CategoryAdd dto)
        {
            var entityId = await _categoryService.Add(dto, UserId.Value);
            return Created($"{Request.Path}/{entityId}", null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryModify dto)
        {
            var entityId = await _categoryService.Update(id, dto, UserId.Value);

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
            await _categoryService.Delete(id, UserId.Value);
            return NoContent();
        }
    }
}
