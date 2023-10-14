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
        private readonly ShopAppContext shopAppContext;
        public EfCoreCartRepository(ShopAppContext context) : base(context)
        {
            shopAppContext = context;
        }

        public void ClearCart(int cartId)
        {
            var cmd = @" delete from CartItems where CartId=@p0";
            shopAppContext.Database.ExecuteSqlRaw(cmd, cartId);
        }

        public void DeleteCartItemFromCart(string userId, int cartItemId)
        {
            var cart = shopAppContext.Carts.Where(c => c.UserId == userId).Include(c => c.CartItems).ThenInclude(cı => cı.Product).FirstOrDefault();
            if (cart != null)
            {
                var cartItem = cart.CartItems.Where(cı => cı.Id == cartItemId).FirstOrDefault();

                if (cartItem != null)
                {
                    cart.CartItems.Remove(cartItem);
                    shopAppContext.SaveChanges();
                }
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return shopAppContext.Carts
                            .Include(c => c.CartItems)
                            .ThenInclude(cı => cı.Product).FirstOrDefault(c => c.UserId == userId);
        }

        public override void Update(Cart cart)
        {
            shopAppContext.Carts.Update(cart);
            shopAppContext.SaveChanges();
        }
    }
}
