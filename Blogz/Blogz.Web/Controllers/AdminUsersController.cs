using Blogz.Web.Models.Domain.Entities;
using Blogz.Web.Models.ViewModels;
using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var userViewModel = new UserViewModel();

            userViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                userViewModel.Users.Add(new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    EmailAdress = user.Email
                });
            }

            return View(userViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> List(UserViewModel request)
        {
            var appUser = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var userResult = await userManager.CreateAsync(appUser, request.Password);

            if (userResult != null)
            {
                if (userResult.Succeeded)
                {
                    var roles = new List<string> { "User" };

                    if (request.IsAdmin)
                    {
                        roles.Add("Admin");
                    }

                    userResult = await userManager.AddToRolesAsync(appUser, roles);

                    if (userResult != null && userResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);

                if (result != null && result.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");

                }
            }

            return View();
        }
    }
}
