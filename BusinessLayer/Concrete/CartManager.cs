using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _cartRepository.Add(new Cart() { UserId = userId });
        }
    }
}
