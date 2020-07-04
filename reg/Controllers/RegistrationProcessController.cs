using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reg.Exceptions;
using reg.Models.RegistrationProcess;
using reg.Services;

namespace reg.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class RegistrationProcessController : ControllerBase
    {
        private readonly IGrpcGreeterClient _service;

        public RegistrationProcessController(IGrpcGreeterClient service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegistrationProcessModel model)
        {
            try
            {
                model.OwnerID = Guid.Parse(User.Identity.Name);
                await _service.AddRegistrationProcessAsync(model);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
