using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KSquare.DMS.WebUI.Models;
using KSquare.DMS.Services.ServicesImpl;
using KSquare.DMS.Services.Interfaces;
using KSquare.DMS.Domain.Models.RequestModel;
using KSquare.DMS.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace KSquare.DMS.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Validate(LoginRequestModel loginrequest)
        {

            if (!ModelState.IsValid)
                return BadRequest("One or more required parameters not passed.");

            ResponseModel<UserModel> responseModel =  await _userService.VerifyUser(loginrequest);
            if (responseModel.IsError)
            {
                return BadRequest(responseModel);
            }
            else
            {
                var responseTokenModel =  await _tokenService.GenerateTokens(responseModel.Model);

                //return Ok(responseTokenModel);  
                return Json(new { status = true, message = "Successfully Login",responseTokenModel });
            }



        }
    }
}