using System;
using System.Collections.Generic;
using cars.Models;

namespace cars.ViewModels
{
    public class CarViewModel
    {
        public IEnumerable<Car> cars { get; set; }
        public IEnumerable<Category> categories { get; set; }

        public FilterCarViewModel filterCarViewModel { get; set; }
    }
}
