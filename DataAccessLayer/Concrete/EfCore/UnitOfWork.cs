using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopAppContext shopAppContext;

        public UnitOfWork(ShopAppContext context)
        {
            shopAppContext = context;
        }

        private EfCoreCartRepository cartRepository;
        private EfCoreOrderRepository orderRepository;
        private EfCoreCategoryRepository categoryRepository;
        private EfCoreProductRepository productRepository;

                                                            //! EfCoreProductRepository'den nesne üretildiyse önceden onu göndeririz, eğer üretilmediyse nesneyi üretelim ve gönderelim
        public IProductRepository ProductRepository => productRepository ??= new EfCoreProductRepository(shopAppContext);

        public ICategoryRepository CategoryRepository => categoryRepository ??= new EfCoreCategoryRepository(shopAppContext);

        public ICartRepository CartRepository => cartRepository ??= new EfCoreCartRepository(shopAppContext);

        public IOrderRepository OrderRepository => orderRepository ??= new EfCoreOrderRepository(shopAppContext);

        public void Dispose()
        {
            shopAppContext.Dispose();
        }

        public void Save()
        {
            shopAppContext.SaveChanges();
        }
    }
}
