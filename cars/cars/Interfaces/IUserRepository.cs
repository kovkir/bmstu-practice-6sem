using System;
using cars.Models;
using System.Collections.Generic;

namespace cars.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);

        IEnumerable<User> GetByPermission(string permission);
    }
}
