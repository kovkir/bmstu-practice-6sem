using System;
using cars.Models;
using cars.Interfaces;
using System.Collections.Generic;
using System.Linq;
using cars.ViewModels;

namespace cars.Services
{
    public interface ICarService
    {
        void Add(Car car);
        void Delete(Car car);
        void Update(Car car);

        Car GetByID(int id);

        IEnumerable<Car> GetAll();
        IEnumerable<Car> GetByBrand(string brand);
        IEnumerable<Car> GetByModel(string model);
        IEnumerable<Car> GetByMinPrice(uint minPrice);
        IEnumerable<Car> GetByMaxPrice(uint maxPrice);

        void AddUserCar(int userId, int carId);
        void DeleteUserCar(int userId, int carId);

        IEnumerable<UserCar> GetAllUserCar();
        UserCar GetUserCar(int userId, int carId);

        IEnumerable<Car> GetMyCarsByUserId(int userId);
        IEnumerable<Car> GetMyCarsByUserLogin(string userLogin);

        IEnumerable<Car> GetByParameters(string brand, string model, uint minPrice, uint maxPrice, string categoryName);
        IEnumerable<Car> GetSortCarsByOrder(IEnumerable<Car> cars, CarSortState sortOrder);

        IEnumerable<Car> UpdateMyCars(IsUpdata isUpdate, int userId, int carId);
        void DeleteMyCarsByUserLogin(string userLogin);
    }

    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CarService(ICarRepository carRepository,
                          IUserRepository userRepository,
                          ICategoryRepository categoryRepository)
        {
            _carRepository = carRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        private bool IsExist(Car car)
        {
            return _carRepository.GetAll().FirstOrDefault(elem =>
                    elem.Brand == car.Brand &&
                    elem.Model == car.Model) != null;
        }

        private bool IsNotExist(int id)
        {
            return _carRepository.GetByID(id) == null;
        }

        public void Add(Car car)
        {
            if (IsExist(car))
                throw new Exception("Машина с таким названием уже существует");
            else
                _carRepository.Add(car);
        }

        public void Delete(Car car)
        {
            if (IsNotExist(car.Id))
                throw new Exception("Такой машины не существует");
            else
                _carRepository.Delete(car.Id);
        }

        public void Update(Car car)
        {
            if (IsNotExist(car.Id))
                throw new Exception("Такой машины не существует");
            else
                _carRepository.Update(car);
        }

        public IEnumerable<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public Car GetByID(int id)
        {
            return _carRepository.GetByID(id);
        }

        public IEnumerable<Car> GetByMaxPrice(uint maxPrice)
        {
            return _carRepository.GetByMaxPrice(maxPrice);
        }

        public IEnumerable<Car> GetByMinPrice(uint minPrice)
        {
            return _carRepository.GetByMinPrice(minPrice);
        }

        public IEnumerable<Car> GetByBrand(string brand)
        {
            return _carRepository.GetByBrand(brand);
        }

        public IEnumerable<Car> GetByModel(string model)
        {
            return _carRepository.GetByModel(model);
        }



        private bool UserCarIsExist(int userId, int carId)
        {
            return _carRepository.GetAllUserCar().FirstOrDefault(elem =>
                    elem.UserId == userId &&
                    elem.CarId == carId) != null;
        }

        private bool UserCarIsNotExist(int userId, int carId)
        {
            return _carRepository.GetUserCar(userId, carId) == null;
        }

        public void AddUserCar(int userId, int carId)
        {
            if (UserCarIsExist(userId, carId) == false)
                _carRepository.AddUserCar(userId, carId);
        }

        public void DeleteUserCar(int userId, int carId)
        {
            if (UserCarIsNotExist(userId, carId) == false)
                _carRepository.DeleteUserCar(userId, carId);
        }

        public IEnumerable<UserCar> GetAllUserCar()
        {
            return _carRepository.GetAllUserCar();
        }

        public UserCar GetUserCar(int userId, int carId)
        {
            return _carRepository.GetUserCar(userId, carId);
        }

        public IEnumerable<Car> GetMyCarsByUserId(int userId)
        {
            return _carRepository.GetMyCarsByUserId(userId);
        }

        public IEnumerable<Car> GetMyCarsByUserLogin(string userLogin)
        {
            User user = _userRepository.GetByLogin(userLogin);
            IEnumerable<Car> myCars;

            if (user == null)
                myCars = null;
            else
                myCars = _carRepository.GetMyCarsByUserId(user.Id);

            return myCars;
        }

        public IEnumerable<Car> GetByParameters(string brand, string model, uint minPrice, uint maxPrice, string categoryName)
        {
            IEnumerable<Car> cars = _carRepository.GetAll();

            if (categoryName != null)
            {
                int categoryId = _categoryRepository.GetByName(categoryName).Id;

                if (cars.Count() != 0)
                    cars = cars.Where(elem => elem.CategoryId == categoryId);
            }

            if (cars.Count() != 0 && brand != null)
                cars = cars.Where(elem => elem.Brand == brand);

            if (cars.Count() != 0 && model != null)
                cars = cars.Where(elem => elem.Model == model);

            if (cars.Count() != 0 && minPrice != 0)
                cars = cars.Where(elem => elem.Price >= minPrice);

            if (cars.Count() != 0 && maxPrice != 0)
                cars = cars.Where(elem => elem.Price <= maxPrice);

            return cars;
        }

        public IEnumerable<Car> GetSortCarsByOrder(IEnumerable<Car> cars, CarSortState sortOrder)
        {
            IEnumerable<Car> needCars = sortOrder switch
            {
                CarSortState.IdDesc => cars.OrderByDescending(elem => elem.Id),

                CarSortState.BrandAsc => cars.OrderBy(elem => elem.Brand),
                CarSortState.BrandDesc => cars.OrderByDescending(elem => elem.Brand),

                CarSortState.ModelAsc => cars.OrderBy(elem => elem.Model),
                CarSortState.ModelDesc => cars.OrderByDescending(elem => elem.Model),

                CarSortState.CategoryNameAsc => cars.OrderBy(elem => _categoryRepository.GetByID(elem.CategoryId).Name),
                CarSortState.CategoryNameDesc => cars.OrderByDescending(elem => _categoryRepository.GetByID(elem.CategoryId).Name),

                CarSortState.PriceAsc => cars.OrderBy(elem => elem.Price),
                CarSortState.PriceDesc => cars.OrderByDescending(elem => elem.Price),

                _ => cars.OrderBy(elem => elem.Id)
            };

            return needCars;
        }

        public IEnumerable<Car> UpdateMyCars(IsUpdata isUpdate, int userId, int carId)
        {
            if (isUpdate == IsUpdata.CarIsAdded)
            {
                AddUserCar(userId, carId);
            }
            else if (isUpdate == IsUpdata.CarIsDeleted)
            {
                DeleteUserCar(userId, carId);
            }

            return _carRepository.GetMyCarsByUserId(userId);
        }

        public void DeleteMyCarsByUserLogin(string userLogin)
        {
            User user = _userRepository.GetByLogin(userLogin);

            IEnumerable<UserCar> userCars = _carRepository.GetAllUserCar()
                .Where(elem => elem.UserId == user.Id);

            foreach (UserCar userCar in userCars)
            {
                _carRepository.DeleteUserCar(userCar.UserId, userCar.CarId);
            }
        }
    }
}
