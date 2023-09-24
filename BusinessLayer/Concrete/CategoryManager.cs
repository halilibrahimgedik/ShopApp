using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        // TODO iş kuralları uygulanacak

        public void Add(Category t)
        {           
            _categoryRepository.Add(t);
        }

        public void Delete(Category t)
        {
            _categoryRepository.Delete(t);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int? id)
        {
            return _categoryRepository.GetById(id);
        }

        public void Update(Category t)
        {
            _categoryRepository.Update(t);
        }
    }
}
