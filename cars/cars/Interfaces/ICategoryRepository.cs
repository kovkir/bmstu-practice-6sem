using System;
using System.Collections.Generic;
using cars.Models;

namespace cars.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByName(string name);
    }
}
