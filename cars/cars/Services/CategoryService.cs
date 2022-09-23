using System;
using cars.Models;
using cars.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace cars.Services
{
    public interface ICategoryService
    {
        void Add(Category category);
        void Delete(Category category);
        void Update(Category category);

        Category GetByID(int id);
        Category GetByName(string name);

        IEnumerable<Category> GetAll();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private bool IsExist(Category category)
        {
            return _categoryRepository.GetAll().FirstOrDefault(elem =>
                    elem.Name == category.Name) != null;
        }

        private bool IsNotExist(int id)
        {
            return _categoryRepository.GetByID(id) == null;
        }

        public void Add(Category category)
        {
            if (IsExist(category))
                throw new Exception("Категория с таким названием уже существует");
            else
                _categoryRepository.Add(category);
        }

        public void Delete(Category category)
        {
            if (IsNotExist(category.Id))
                throw new Exception("Такой категории не существует");
            else
                _categoryRepository.Delete(category.Id);
        }

        public void Update(Category category)
        {
            if (IsNotExist(category.Id))
                throw new Exception("Такой категории не существует");
            else
                _categoryRepository.Update(category);
        }

        public Category GetByID(int id)
        {
            return _categoryRepository.GetByID(id);
        }

        public Category GetByName(string name)
        {
            return _categoryRepository.GetByName(name);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }
    }
}
