using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cars.ViewModels;
using cars.Services;
using Microsoft.AspNetCore.Mvc;
using cars.Models;

namespace cars.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "DoStart";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HomeViewModel model)
        {
            ViewBag.Title = "Find";

            if (ModelState.IsValid)
            {
                uint minPrice = checkForNull(model.filterCarViewModel.minPrice);
                uint maxPrice = checkForNull(model.filterCarViewModel.maxPrice);

                return RedirectToAction("GetAllCars", "Car",
                    new
                    {
                        brand = model.filterCarViewModel.brand,
                        model = model.filterCarViewModel.model,
                        minPrice = minPrice,
                        maxPrice = maxPrice,
                        categoryName = model.filterCarViewModel.categoryName
                    });
            }
            else
                ModelState.AddModelError("", "Некорректные данные");

            return View(model);
        }

        private uint checkForNull(uint? value)
        {
            uint newValue;

            if (value != null)
                newValue = (uint) value;
            else
                newValue = 0;

            return newValue;
        }
    }
}
