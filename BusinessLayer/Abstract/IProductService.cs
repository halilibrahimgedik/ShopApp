using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProductService
    {
        Task<Product> GetById(int id);

        void Delete(Product t);

        Task DeleteAsync(Product t);

        void Update(Product t);

        void Add(Product t);

        Task<Product>AddAsync(Product t);

        Task<List<Product>> GetAll();

        Product GetProductDetails(string url);

        List<Product> ListProductsByCategory(string name, int page, int pageSize);

        int GetCountByCategory(string category);

        List<Product> GetHomePageProducts();

        List<Product> GetSearchResult(string stringForSearch, int page, int PageSize);

        int GetCountBySearchResult(string stringForSearch);

        Product GetProductByIdWithCategories(int id);

        void Update(Product p, int[] categoryIds);

        void Add(Product p, int[] categoryIds);

        Task UpdateAsync(Product entityToUpdate,Product entity);
    }
}
