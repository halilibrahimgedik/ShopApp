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
        private readonly IUnitOfWork unitOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                unitOfWork.CartRepository.Update(cart);
                unitOfWork.Save();
            }
        }

        public void ClearCart(int cartId)
        {
            unitOfWork.CartRepository.ClearCart(cartId);
        }

        public void DeleteCartItemFromCart(string userId, int cartItemId)
        {
            unitOfWork.CartRepository.DeleteCartItemFromCart(userId,cartItemId);
        }

        public Cart GetCartByUserId(string userId)
        {
            return unitOfWork.CartRepository.GetCartByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            unitOfWork.CartRepository.Add(new Cart() { UserId = userId });
            unitOfWork.Save();
        }
    }
}
