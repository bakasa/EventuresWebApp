using System.Linq;
using Eventures.Models;
using Eventures.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Eventures.Web.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<EventuresUser> signIn;
        private UserManager<EventuresUser> userManager;

        public AccountController(SignInManager<EventuresUser> signIn, UserManager<EventuresUser> userManager)
        {
            this.signIn = signIn;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.signIn.SignOutAsync().Wait();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LogInInputModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var user = this.userManager.Users.FirstOrDefault(u => u.UserName == model.Username);
            if (user == null) //user does not exists
            {
                return this.BadRequest("Invalid username or password.");
            }

            SignInResult result = null;
            if (model.RememberMe)
            {
                result = this.signIn.PasswordSignInAsync(model.Username, model.Password, true, false).Result;
            }
            else
            {
                result = this.signIn.PasswordSignInAsync(model.Username, model.Password, false, false).Result;
            }

            if (result == SignInResult.Success) //successfully logged in
            {
                return this.RedirectToAction("Index", "Home");
            }
            else
            {
                return this.BadRequest("Invalid username or password.");
            }  
        }

        public IActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var user = new EventuresUser
                {
                    Email = model.Email,
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UniqueCitizenNumber = model.UCN,
                };

                var result = this.userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();

                var roleResult = this.userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
                if (roleResult.Errors.Any())
                {
                    return this.View();
                }

                if (result.Succeeded)
                {
                    this.signIn.SignInAsync(user, true).Wait();
                    return this.RedirectToAction("Index", "Home");
                }
            }

            return this.View();
        }
    }
}