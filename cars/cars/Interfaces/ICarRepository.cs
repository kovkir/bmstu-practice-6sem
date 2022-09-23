using System;
using System.Collections.Generic;
using cars.Models;

namespace cars.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        IEnumerable<Car> GetByBrand(string brand);
        IEnumerable<Car> GetByModel(string model);

        IEnumerable<Car> GetByMinPrice(uint minPrice);
        IEnumerable<Car> GetByMaxPrice(uint maxPrice);

        void AddUserCar(int userId, int carId);
        void DeleteUserCar(int userId, int carId);

        IEnumerable<UserCar> GetAllUserCar();
        UserCar GetUserCar(int userId, int carId);

        IEnumerable<Car> GetMyCarsByUserId(int userId);
    }
}
