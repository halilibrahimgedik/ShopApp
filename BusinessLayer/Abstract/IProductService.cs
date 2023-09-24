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
        Product GetById(int id);

        void Delete(Product t);

        void Update(Product t);

        void Add(Product t);

        List<Product> GetAll();

        Product GetProductDetails(int id);

        List<Product> ListProductsByCategory(string name);
    }
}
