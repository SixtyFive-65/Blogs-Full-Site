using Blogz.Web.Models.Domain.Entities;
using Blogz.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

        [HttpGet]
        public IActionResult Login( string returnUrl)
        {
            var loginModel = new LoginModel { ReturnUrl = returnUrl }; 
            
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var loginResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (loginResult != null && loginResult.Succeeded)
            {
                if(!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
