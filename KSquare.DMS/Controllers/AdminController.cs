using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSquare.DMS.Domain.Models;
using KSquare.DMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSquare.DMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region Client

        [Authorize]
        [HttpPost("Client")]
        public async Task<IActionResult> AddClient(ClientModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("One or more required parameters not passed.");

            var response = new ResponseModel<ClientModel>();
            if (requestModel.Id == 0)
            {
                response = await _adminService.AddClient(requestModel);
            }
            else
            {
                response = await _adminService.UpdateClient(requestModel);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Client")]
        public async Task<IActionResult> GetClients()
        {
            var response = await _adminService.GetAllClients();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Client/{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var response = await _adminService.GetClient(id);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("Client/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var response = await _adminService.DeleteClient(id);
            return Ok(response);
        }

        #endregion
    }
}