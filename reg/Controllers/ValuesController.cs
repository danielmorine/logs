using Microsoft.AspNetCore.Mvc;

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
    }
}
