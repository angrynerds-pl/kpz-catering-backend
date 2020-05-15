using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Catering_API.Controllers
{
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<Database.Entities.Admin> _userManager;
        private readonly SignInManager<Database.Entities.Admin> _signInManager;
        public AdminController(
           UserManager<Database.Entities.Admin> userManager,
           SignInManager<Database.Entities.Admin> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Route("Page")]
        public void Index()
        {
            Response.Redirect("https://hungrynerds2.z13.web.core.windows.net/admin/page");
        }
        [Route("UserInfo")]
        [Authorize]
        public async void UserInfo()
        {
            var user =
                await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            if (user == null)
            {
                RedirectToAction("Login");
            }
            //login functionality  

            Response.Redirect("https://hungrynerds2.z13.web.core.windows.net/");
        }
        [Route("Login")]
        [HttpGet]
        public void Login()
        {
            Response.Redirect("https://hungrynerds2.z13.web.core.windows.net/admin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Database.Entities.Admin admin)
        {

            //login functionality  
            var user = await _userManager.FindByNameAsync(admin.login);

            if (user != null)
            {
                //sign in  
                var signInResult = await _signInManager.PasswordSignInAsync
                                   (user, admin.haslo, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        [Route("Logout")]
        public async Task<IActionResult> LogOut(string username, string password)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}