using System;
using System.Collections.Generic;

namespace cars.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T model);
        void Update(T model);
        void Delete(int id);

        IEnumerable<T> GetAll();
        T GetByID(int id);
    }
}
