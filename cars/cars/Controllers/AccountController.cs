using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cars.ViewModels;
using cars.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using cars.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace cars.Controllers
{
    public class AccountController : Controller
    {
        IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Title = "Register";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewBag.Title = "Register";

            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User
                    {
                        Login = model.Login,
                        Password = model.Password,
                        Permission = "user"
                    };

                    userService.Add(user);

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
                ModelState.AddModelError("", "Некорректные данные");

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewBag.Title = "Login";

            if (ModelState.IsValid)
            {
                User user = userService.GetByLogin(model.Login);

                if (user != null && user.Password == model.Password)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Permission)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
