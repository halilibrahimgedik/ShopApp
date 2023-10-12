using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EfCore
{
    public class EfCoreGenericRepository<T> : IRepository<T> where T : class
    {
        public void Add(T t)
        {
            using (var context = new ShopAppContext())
            {
                context.Set<T>().Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(T t)
        {
            using (var context = new ShopAppContext())
            {
                context.Set<T>().Remove(t);
                context.SaveChanges();
            }
        }

        public List<T> GetAll()
        {
            using (var context = new ShopAppContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public T GetById(int id)
        {
            using (var context = new ShopAppContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public virtual void Update(T t)
        {
            using (var context = new ShopAppContext())
            {
                context.Set<T>().Update(t);
                //context.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
