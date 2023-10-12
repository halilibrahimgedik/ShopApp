using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
        void DeleteCartItemFromCart(string userId, int cartItemId);
        Cart GetCartByUserId(string userId);
    }
}
