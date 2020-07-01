using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace reg.Controllers
{
    [Route("api/v1/[controller]")]
    public class RegisterController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            return Ok("Logado");
        }
    }
}
