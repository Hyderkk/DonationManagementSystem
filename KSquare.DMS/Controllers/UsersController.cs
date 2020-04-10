using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSquare.DMS.Domain.Models.RequestModel;
using KSquare.DMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSquare.DMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost()]
        public async Task<IActionResult> AddUser(AddUserRequestModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("One or more required parameters not passed.");

            var response = await _userService.AddUser(requestModel);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("One or more required parameters not passed.");

            var responseModel = await _userService.VerifyUser(requestModel);
            if(responseModel.IsError)
            {
                return BadRequest(responseModel);
            }
            else
            {
                var responseTokenModel = await _tokenService.GenerateTokens(responseModel.Model);

                return Ok(responseTokenModel);
            }
          //  return Ok(response);
        }

        [Authorize]
        [HttpGet("UserCategories")]
        public async Task<IActionResult> GetUserCategories()
        {
            var response = await _userService.GetUserCategories();
            return Ok(response);
        }
    }
}