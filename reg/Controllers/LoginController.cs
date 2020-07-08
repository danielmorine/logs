using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reg.Models.Login;
using reg.Services;

namespace reg.Controllers
{
    [Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
       
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LoginModel model)
        {
            try
            {
                return Ok(await _loginService.LoginAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}
