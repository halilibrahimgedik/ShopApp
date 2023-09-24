using DataAccessLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EfCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product>, IProductRepository
    {
        public List<Product> GetPopulerProducts()
        {
            throw new NotImplementedException();
        }
    }
}
