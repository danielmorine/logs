using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reg.Services;

namespace reg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IGrpcGreeterClient _client;
        public ValuesController(IGrpcGreeterClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("values");
        }

        [HttpGet]
        [Route("call")]
        public async Task<IActionResult> GetMessage()
        {
            try
            {
                return Ok(await _client.CallAsync());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
