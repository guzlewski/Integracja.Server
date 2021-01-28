using System;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    { 
        [HttpGet("[action]/{guid}")]
        public IActionResult Join(Guid guid)
        {
            throw new NotImplementedException();
        }

        [HttpGet("[action]/{guid}")]
        public IActionResult Leave(Guid guid)
        {
            throw new NotImplementedException();
        }

        [HttpGet("[action]/{guid}")]
        public IActionResult GetInfo(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
