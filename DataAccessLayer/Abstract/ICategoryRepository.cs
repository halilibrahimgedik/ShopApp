using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void DeleteProductFromCategories(int productId, int categoryId);

        Category GetByIdWithProducts(int categoryId);
    }


}
