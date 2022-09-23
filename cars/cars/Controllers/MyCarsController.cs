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
    public class MyCarsController : Controller
    {
        ICarService carService;
        ICategoryService categoryService;
        IUserService userService;

        public MyCarsController(ICarService carService,
                                ICategoryService categoryService,
                                IUserService userService)
        {
            this.carService = carService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public IActionResult GetMyCars(CarSortState sortOrder = CarSortState.IdAsc,
                                       IsUpdata isUpdate = IsUpdata.IsNotUpdate, int carId = 0)
        {
            ViewBag.Title = "MyCars";

            ViewData["IdSort"]           = sortOrder == CarSortState.IdAsc           ? CarSortState.IdDesc           : CarSortState.IdAsc;
            ViewData["BrandSort"]        = sortOrder == CarSortState.BrandAsc        ? CarSortState.BrandDesc        : CarSortState.BrandAsc;
            ViewData["ModelSort"]        = sortOrder == CarSortState.ModelAsc        ? CarSortState.ModelDesc        : CarSortState.ModelAsc;
            ViewData["CategoryNameSort"] = sortOrder == CarSortState.CategoryNameAsc ? CarSortState.CategoryNameDesc : CarSortState.CategoryNameAsc;
            ViewData["PriceSort"]        = sortOrder == CarSortState.PriceDesc       ? CarSortState.PriceAsc         : CarSortState.PriceDesc;

            User user = userService.GetByLogin(User.Identity.Name);
            IEnumerable<Car> myCars = carService.UpdateMyCars(isUpdate, user.Id, carId);

            MyCarsViewModel myCarsViewModel = new MyCarsViewModel
            {
                myCars = carService.GetSortCarsByOrder(myCars, sortOrder),
                categories = categoryService.GetAll(),

                car = carService.GetByID(carId),

                _isUpdate = isUpdate
            };

            return View(myCarsViewModel);
        }
    }
}
