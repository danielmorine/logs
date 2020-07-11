using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reg.Services;

namespace reg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("values");
        }


        [HttpGet]
        [Route("call")]
        public async Task<IActionResult> GetMessage([FromServices] GrpcGreeterClient client)
        {
            return Ok(await client.CallAsync());
        }
    }
}
