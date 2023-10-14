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
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order>, IOrderRepository
    {
        private readonly ShopAppContext shopAppContext;
        public EfCoreOrderRepository(ShopAppContext context) : base(context)
        {
            shopAppContext = context;
        }


        public List<Order> GetOrders(string userId)
        {
            var orders = shopAppContext.Orders.Include(o => o.OrderItems)
                                        .ThenInclude(oı => oı.Product)
                                        .AsQueryable();

            if (string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(o => o.UserId == userId);
            }

            return orders.ToList();
        }
    }
}
