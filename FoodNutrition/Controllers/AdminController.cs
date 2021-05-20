using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.Model;
using FoodNutrition.Helper;
using FoodNutrition.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Controllers
{
    [ApiController]
    [Route("api/admin/")]
    public class AdminController:ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
        }
        [Authorize]
        [HttpPost,Route("add")]
        public async Task<ActionResult> Add(Admin admin)
        {
            try
            {
                await adminService.AddAmin(admin);
                return Ok("Add successful");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost,Route("authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticateRequest request)
        {
            try
            {
                AuthenticateResponse response = await adminService.Authenticate(request);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
