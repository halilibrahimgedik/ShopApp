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

        List<Product> GetPopulerProducts();

        List<Product> ListProductsByCategory(string name,int page,int pageSize);

        int GetCountByCategory(string category);
    }
}
