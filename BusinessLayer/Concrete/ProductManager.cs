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
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public void Add(Product t)
        {
            _productRepository.Add(t);
        }

        public void Delete(Product t)
        {
            _productRepository.Delete(t);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        // Kategoriye göre kaç ürün varsa onu döndürecek metot
        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public int GetCountBySearchResult(string stringForSearch)
        {
            return _productRepository.GetCountBySearchResult(stringForSearch);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productRepository.GetHomePageProducts();
        }

        public Product GetProductByIdWithCategories(int id)
        {
            return _productRepository.GetProductByIdWithCategories(id);
        }

        // ürünlerle birlikte kategorileride getirir
        public Product GetProductDetails(string url)
        {
            return _productRepository.GetProductDetails(url);
        }

        public List<Product> GetSearchResult(string stringForSearch, int page, int PageSize)
        {
            return _productRepository.GetSearchResult(stringForSearch,page,PageSize);
        }

        public List<Product> ListProductsByCategory(string name, int page, int pageSize)
        {
            return _productRepository.ListProductsByCategory(name,page,pageSize);
        }

        public void Update(Product t)
        {
            _productRepository.Update(t);
        }

        public void Update(Product p, int[] categoryIds)
        {
            _productRepository.Update(p, categoryIds);
        }
    }
}
