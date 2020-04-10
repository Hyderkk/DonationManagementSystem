using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSquare.DMS.Domain.Models;
using KSquare.DMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSquare.DMS.WebUI.Controllers
{
    public class AdminController : Controller
    {

        private readonly IAdminService _adminService;
        private readonly ITokenService _tokenService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var beneficiaries = await _adminService.GetAllBeneficiaries();
            return View("Beneficiary", beneficiaries);
        }

        public async Task<IActionResult> GetAll()
        {
            var beneficiaries = await _adminService.GetAllBeneficiaries();
            return View(beneficiaries);
        }

        [HttpPost]
        public async Task<IActionResult> AddBeneficiary(BeneficiaryModel requestModel)
        {
            //if(!ModelState.IsValid)
            //{

            //}
            var response = new ResponseModel<BeneficiaryModel>();
            if (requestModel.Id == 0)
            {
                response = await _adminService.AddBeneficiary(requestModel);
            }
            else
            {
                response = await _adminService.UpdateBeneficiary(requestModel);
            }

            return Json(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeneficiary(int id)
        {
            //if(!ModelState.IsValid)
            //{

            //}

            var response = await _adminService.GetBeneficiary(id);
            return Json(response);
        }

        public IActionResult GetCities()
        {
            var cities = _adminService.GetCities();
            return Json(cities);
        }
    }
}