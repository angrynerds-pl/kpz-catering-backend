using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Catering_API.Models;
using KPZ_Catering_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Catering_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        [HttpPost("Register")]
        public String Registration([FromBody] Entities.Account account)
        {
            return Database.Logic.DatabaseController.putAccount(account);
        }

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var account = _accountService.Authenticate(model.Username, model.Password);

            if (account == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(account);
        }
    }
}