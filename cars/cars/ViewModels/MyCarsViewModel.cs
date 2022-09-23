using System;
using System.Collections.Generic;
using cars.Models;

namespace cars.ViewModels
{
    public enum IsUpdata
    {
        CarIsAdded,
        CarIsDeleted,

        IsNotUpdate
    }

    public class MyCarsViewModel
    {
        public IEnumerable<Car> myCars { get; set; }
        public IEnumerable<Category> categories { get; set; }

        public Car car { get; set; }

        public IsUpdata _isUpdate { get; set; }
    }
}
