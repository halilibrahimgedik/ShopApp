using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);

        void Delete(T t);

        void Update(T t);

        void Add(T t);

        Task AddAsync(T t);

        Task<List<T>> GetAll();
    }
}
