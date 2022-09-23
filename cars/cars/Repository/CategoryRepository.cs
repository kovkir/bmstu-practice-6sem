using System;
using cars.Models;
using cars.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace cars.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext _appDBContext;

        public CategoryRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Category model)
        {
            try
            {
                _appDBContext.Categories.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении категории машин");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Category category = _appDBContext.Categories.Find(id);

                if (category != null)
                {
                    _appDBContext.Categories.Remove(category);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении категории машин");
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return _appDBContext.Categories.ToList();
        }

        public Category GetByID(int id)
        {
            return _appDBContext.Categories.Find(id);
        }

        public Category GetByName(string name)
        {
            return _appDBContext.Categories.FirstOrDefault(elem => elem.Name == name);
        }

        public void Update(Category model)
        {
            try
            {
                var curModel = _appDBContext.Categories.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);

                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при обновлении категории машин");
            }
        }
    }
}
