using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using itmanager.Models;

namespace itmanager.Controllers
{
    // Controls user accounts
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public TicketContext context;

        public AccountController(UserManager<User> userMngr,
            SignInManager<User> signInMngr, TicketContext ctx)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            context = ctx;
        }

        [HttpGet] // Register View with a list of stores to pick from
        public IActionResult Register()
        {
            ViewBag.Stores = context.Stores.ToList(); 
            return View();
        }

        [HttpPost] // If valid creates a new user and signs them in
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new itmanager.Models.User
                {
                    UserName = model.Username,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email,
                    StoreId = model.StoreId
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            ViewBag.Stores = context.Stores.ToList();
            return View(model);
        }

        // Logs user out
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet] // Initial login view, saves url if you were redirected
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        // Post for logging in. If redirected to the login page it will redirect you back to where you were going
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                        Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Ticket", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }

        // Not used atm
        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}