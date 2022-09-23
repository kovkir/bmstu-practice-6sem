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
    public class OrderController : Controller
    {
        IUserService userService;
        ICarService carService;

        public OrderController(IUserService userService,
                               ICarService carService)
        {
            this.userService = userService;
            this.carService = carService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            ViewBag.Title = "Order";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderViewModels model)
        {
            ViewBag.Title = "Order";

            if (ModelState.IsValid)
            {
                carService.DeleteMyCarsByUserLogin(User.Identity.Name);

                return RedirectToAction("Success", "Order");
            }
            else
                ModelState.AddModelError("", "Некорректные данные");

            return View(model);
        }

        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.Title = "Success";

            return View();
        }
    }
}
