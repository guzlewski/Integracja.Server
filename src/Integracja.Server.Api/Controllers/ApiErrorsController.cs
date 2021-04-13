using System;
using System.Collections.Generic;
using Integracja.Server.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiErrorsController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<KeyValuePair<int, ErrorCode>> GetAll()
        {
            foreach (ErrorCode errorCode in Enum.GetValues(typeof(ErrorCode)))
            {
                yield return new KeyValuePair<int, ErrorCode>((int)errorCode, errorCode);
            }
        }
    }
}
