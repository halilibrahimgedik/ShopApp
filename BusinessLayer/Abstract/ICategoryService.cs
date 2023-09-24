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
        Category GetById(int? id);

        void Delete(Category t);

        void Update(Category t);

        void Add(Category t);

        List<Category> GetAll();
    }
}
