using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cars.ViewModels;
using cars.Interfaces;
using cars.Services;
using Microsoft.AspNetCore.Mvc;
using cars.Models;

namespace cars.Controllers
{
    public class CarController : Controller
    {
        ICarService carService;
        ICategoryService categoryService;

        public CarController(ICarService carService,
                             ICategoryService categoryService)
        {
            this.carService = carService;
            this.categoryService = categoryService;
        }

        public IActionResult GetAllCars(CarSortState sortOrder = CarSortState.IdAsc,
                                        string brand = null, string model = null,
                                        uint minPrice = 0, uint maxPrice = 0, string categoryName = null)
        {
            ViewBag.Title = "Cars";

            ViewData["IdSort"]           = sortOrder == CarSortState.IdAsc           ? CarSortState.IdDesc           : CarSortState.IdAsc;
            ViewData["BrandSort"]        = sortOrder == CarSortState.BrandAsc        ? CarSortState.BrandDesc        : CarSortState.BrandAsc;
            ViewData["ModelSort"]        = sortOrder == CarSortState.ModelAsc        ? CarSortState.ModelDesc        : CarSortState.ModelAsc;
            ViewData["CategoryNameSort"] = sortOrder == CarSortState.CategoryNameAsc ? CarSortState.CategoryNameDesc : CarSortState.CategoryNameAsc;
            ViewData["PriceSort"]        = sortOrder == CarSortState.PriceDesc       ? CarSortState.PriceAsc         : CarSortState.PriceDesc;

            IEnumerable<Car> cars = carService.GetByParameters(brand, model, minPrice, maxPrice, categoryName);

            CarViewModel carViewModel = new CarViewModel
            {
                cars = carService.GetSortCarsByOrder(cars, sortOrder),
                categories = categoryService.GetAll(),

                filterCarViewModel = new FilterCarViewModel
                {
                    brand = brand,
                    model = model,
                    minPrice = minPrice,
                    maxPrice = maxPrice,
                    categoryName = categoryName
                }
            };

            return View(carViewModel);
        }
    }
}
