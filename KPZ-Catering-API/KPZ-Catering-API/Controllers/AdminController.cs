using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Catering_API.Models;
using KPZ_Catering_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Catering_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var admin = _adminService.Authenticate(model.Username, model.Password);

            if (admin == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(admin);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var admins = _adminService.GetAll();
            return Ok(admins);
        }
    }
}