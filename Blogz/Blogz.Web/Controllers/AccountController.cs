using Blogz.Web.Models.Domain.Entities;
using Blogz.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var identityUser = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            
            var result = await userManager.CreateAsync(identityUser, model.Password); // First Create User 

            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }

            return View();
        }
    }
}
