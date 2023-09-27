using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(string url);

        List<Product> GetSearchResult(string stringForSearch, int page, int PageSize);

        List<Product> ListProductsByCategory(string name,int page,int pageSize);

        int GetCountByCategory(string category);

        List<Product> GetHomePageProducts();

        int GetCountBySearchResult(string stringForSearch);
    }
}
