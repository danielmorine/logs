using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reg.Services;
using System.Threading.Tasks;

namespace reg.Controllers
{
    [Route("api/v1/[controller]")]

    public class TestController : ControllerBase
    {
        private readonly IGrpcGreeterClient _grpcGreeterClient;

        public TestController(IGrpcGreeterClient grpcGreeterClient)
        {
            _grpcGreeterClient = grpcGreeterClient;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _grpcGreeterClient.Say();
            return Ok();
        }
    }
}
