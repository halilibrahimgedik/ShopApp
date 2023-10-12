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
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart>, ICartRepository
    {
        public Cart GetCartByUserId(string userId)
        {
            using (var context = new ShopAppContext())
            {
                return context.Carts
                                .Include(c => c.CartItems)
                                .ThenInclude(cı => cı.Product).FirstOrDefault(c => c.UserId == userId);
            }
        }

        public override void Update(Cart cart)
        {
            using (var context = new ShopAppContext())
            {
                context.Carts.Update(cart);
                context.SaveChanges();
            }
        }
    }
}
