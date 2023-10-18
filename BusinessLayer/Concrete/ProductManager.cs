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
        private readonly IUnitOfWork unitOfWork;
        public ProductManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void Add(Product t)
        {
            unitOfWork.ProductRepository.Add(t);
            unitOfWork.Save();
        }

        public void Add(Product p, int[] categoryIds)
        {
            unitOfWork.ProductRepository.Add(p,categoryIds);
            unitOfWork.Save();
        }

        public async Task<Product> AddAsync(Product t)
        {
            await unitOfWork.ProductRepository.AddAsync(t);
            await unitOfWork.SaveAsync();
            return t;
        }

        public void Delete(Product t)
        {
            unitOfWork.ProductRepository.Delete(t);
            unitOfWork.Save();
        }

        public async Task<List<Product>> GetAll()
        {
            return await unitOfWork.ProductRepository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            return await unitOfWork.ProductRepository.GetById(id);
        }

        // Kategoriye göre kaç ürün varsa onu döndürecek metot
        public int GetCountByCategory(string category)
        {
            return unitOfWork.ProductRepository.GetCountByCategory(category);
        }

        public int GetCountBySearchResult(string stringForSearch)
        {
            return unitOfWork.ProductRepository.GetCountBySearchResult(stringForSearch);
        }

        public List<Product> GetHomePageProducts()
        {
            return unitOfWork.ProductRepository.GetHomePageProducts();
        }

        public Product GetProductByIdWithCategories(int id)
        {
            return unitOfWork.ProductRepository.GetProductByIdWithCategories(id);
        }

        // ürünlerle birlikte kategorileride getirir
        public Product GetProductDetails(string url)
        {
            return unitOfWork.ProductRepository.GetProductDetails(url);
        }

        public List<Product> GetSearchResult(string stringForSearch, int page, int PageSize)
        {
            return unitOfWork.ProductRepository.GetSearchResult(stringForSearch, page, PageSize);
        }

        public List<Product> ListProductsByCategory(string name, int page, int pageSize)
        {
            return unitOfWork.ProductRepository.ListProductsByCategory(name, page, pageSize);
        }

        public void Update(Product t)
        {
            unitOfWork.ProductRepository.Update(t);
            unitOfWork.Save();
        }

        public void Update(Product p, int[] categoryIds)
        {
            unitOfWork.ProductRepository.Update(p, categoryIds);
        }
    }
}
