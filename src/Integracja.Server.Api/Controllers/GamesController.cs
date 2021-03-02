﻿using System;
using Integracja.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : DefaultController
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
