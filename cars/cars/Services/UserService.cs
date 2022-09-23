using System;
using cars.Models;
using cars.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace cars.Services
{
    public interface IUserService
    {
        void Add(User user);
        void Delete(User user);
        void Update(User user);

        User GetByID(int id);
        User GetByLogin(string login);

        IEnumerable<User> GetAll();
        IEnumerable<User> GetByPermission(string permission);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private bool IsExist(User user)
        {
            return _userRepository.GetAll().FirstOrDefault(elem =>
                    elem.Login == user.Login) != null;
        }

        private bool IsNotExist(int id)
        {
            return _userRepository.GetByID(id) == null;
        }

        public void Add(User user)
        {
            if (IsExist(user))
                throw new Exception("Пользователь с таким логином уже существует");

            _userRepository.Add(user);
        }

        public void Delete(User user)
        {
            if (IsNotExist(user.Id))
                throw new Exception("Такого пользователя не существует");

            _userRepository.Delete(user.Id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByID(int id)
        {
            return _userRepository.GetByID(id);
        }

        public User GetByLogin(string login)
        {
            return _userRepository.GetByLogin(login);
        }

        public IEnumerable<User> GetByPermission(string permission)
        {
            return _userRepository.GetByPermission(permission);
        }

        public void Update(User user)
        {
            if (IsNotExist(user.Id))
                throw new Exception("Такого пользователя не существует");

            _userRepository.Update(user);
        }
    }
}
