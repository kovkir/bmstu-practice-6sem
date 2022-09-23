using System;
using cars.Models;
using cars.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace cars.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDBContext _appDBContext;

        public CarRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Car model)
        {
            try
            {
                _appDBContext.Cars.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении машины");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Car car = _appDBContext.Cars.Find(id);

                if (car != null)
                {
                    _appDBContext.Cars.Remove(car);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении машины");
            }
        }

        public IEnumerable<Car> GetAll()
        {
            return _appDBContext.Cars.ToList();
        }

        public Car GetByID(int id)
        {
            return _appDBContext.Cars.Find(id);
        }

        public IEnumerable<Car> GetByMaxPrice(uint maxPrice)
        {
            return _appDBContext.Cars.Where(elem => elem.Price <= maxPrice).ToList();
        }

        public IEnumerable<Car> GetByMinPrice(uint minPrice)
        {
            return _appDBContext.Cars.Where(elem => elem.Price >= minPrice).ToList();
        }

        public IEnumerable<Car> GetByBrand(string brand)
        {
            return _appDBContext.Cars.Where(elem => elem.Brand == brand).ToList();
        }

        public IEnumerable<Car> GetByModel(string model)
        {
            return _appDBContext.Cars.Where(elem => elem.Model == model).ToList();
        }

        public void Update(Car model)
        {
            try
            {
                var curModel = _appDBContext.Cars.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);

                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при обновлении машины");
            }
        }


        public void AddUserCar(int userId, int carId)
        {
            UserCar userCar = new UserCar
            {
                UserId = userId,
                CarId = carId
            };

            try
            {
                _appDBContext.UserCars.Add(userCar);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении связи между пользователем и машиной");
            }
        }

        public void DeleteUserCar(int userId, int carId)
        {
            try
            {
                UserCar userCar = _appDBContext.UserCars
                    .FirstOrDefault(elem => elem.UserId == userId &&
                                            elem.CarId == carId);

                if (userCar != null)
                {
                    _appDBContext.UserCars.Remove(userCar);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении связи между пользователем и машиной");
            }
        }

        public IEnumerable<UserCar> GetAllUserCar()
        {
            return _appDBContext.UserCars.ToList();
        }

        public UserCar GetUserCar(int userId, int carId)
        {
            return _appDBContext.UserCars
                .FirstOrDefault(elem => elem.UserId == userId &&
                                        elem.CarId == carId);
        }

        public IEnumerable<Car> GetMyCarsByUserId(int userId)
        {
            IEnumerable<int> myUserCars = _appDBContext.UserCars
                .Where(elem => elem.UserId == userId)
                .Select(elem => elem.CarId).ToList();

            IEnumerable<Car> myCars = _appDBContext.Cars
                .Where(elem => myUserCars.Contains(elem.Id)).ToList();

            return myCars;
        }
    }
}
