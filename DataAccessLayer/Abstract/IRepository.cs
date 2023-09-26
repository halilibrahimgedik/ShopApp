using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);

        void Delete(T t);

        void Update(T t);

        void Add(T t);

        List<T> GetAll();
    }
}
