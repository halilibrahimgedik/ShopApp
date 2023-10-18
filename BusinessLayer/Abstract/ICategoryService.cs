using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        Task<Category> GetById(int id);

        void Delete(Category t);

        void Update(Category t);

        void Add(Category t);

        Task<Category> AddAsync(Category t);

        Task<List<Category>> GetAll();

        Category GetByIdWithProducts(int categoryId);

        void DeleteProductFromCategories(int productId, int categoryId);
    }
}
