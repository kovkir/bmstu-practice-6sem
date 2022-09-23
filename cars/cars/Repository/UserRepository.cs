using System;
using cars.Models;
using cars.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace cars.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _appDBContext;

        public UserRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(User model)
        {
            try
            {
                _appDBContext.Users.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении пользователя");
            }
        }

        public void Delete(int id)
        {
            try
            {
                User user = _appDBContext.Users.Find(id);

                if (user != null)
                {
                    _appDBContext.Users.Remove(user);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении пользователя");
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _appDBContext.Users.ToList();
        }

        public User GetByID(int id)
        {
            return _appDBContext.Users.Find(id);
        }

        public User GetByLogin(string login)
        {
            return _appDBContext.Users.FirstOrDefault(elem => elem.Login == login);
        }

        public IEnumerable<User> GetByPermission(string permission)
        {
            return _appDBContext.Users.Where(elem => elem.Permission == permission).ToList();
        }

        public void Update(User model)
        {
            try
            {
                var curModel = _appDBContext.Users.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);

                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при обновлении пользователя");
            }
        }
    }
}
