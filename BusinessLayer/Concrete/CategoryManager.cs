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
        private readonly IUnitOfWork unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        // TODO iş kuralları uygulanacak

        public void Add(Category t)
        {
            unitOfWork.CategoryRepository.Add(t);
            unitOfWork.Save();
        }

        public void Delete(Category t)
        {
            unitOfWork.CategoryRepository.Delete(t);
            unitOfWork.Save();
        }

        public void DeleteProductFromCategories(int productId, int categoryId)
        {
            unitOfWork.CategoryRepository.DeleteProductFromCategories(productId,categoryId);
        }

        public List<Category> GetAll()
        {
            return unitOfWork.CategoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return unitOfWork.CategoryRepository.GetById(id);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            return unitOfWork.CategoryRepository.GetByIdWithProducts(categoryId);
        }

        public void Update(Category t)
        {
            unitOfWork.CategoryRepository.Update(t);
            unitOfWork.Save();
        }
    }
}
