﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reg.Exceptions;
using reg.Models.RegistrationProcess;
using reg.Scaffolds;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _service.GetAllAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.GetByID(id));
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

        [HttpPut]
        [Route("archive")]
        public async Task<IActionResult> ArchiveAsync([FromBody] RegistrationProcessArchiveModel model)
        {
            try
            {
                await _service.ArchiveAsync(model);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] RegistrationProcessDeleteModel model)
        {
            try
            {
                await _service.DeleteAsync(model);
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

        [HttpPost]
        [Route("filters")]
        public async Task<IActionResult> GetByFiltersAsync([FromBody] RegistrationProcessFilterModel model)
        {
            try
            {
                return Ok(await _service.GetByFiltersAsync(model));
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
