using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EfCore
{
    public class EfCoreGenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;

        public EfCoreGenericRepository(DbContext context)
        {
            this.context = context;
        }


        public void Add(T t)
        {
            context.Set<T>().Add(t);
        }

        public async Task AddAsync(T t)
        {
            await context.Set<T>().AddAsync(t);
        }

        public void Delete(T t)
        {
            context.Set<T>().Remove(t);
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual void Update(T t)
        {
            context.Set<T>().Update(t);
            //context.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
