using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;

        public CartController(ICartService cartService,UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public IActionResult ShowCart()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var cartVm = new CartVM()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(cartItem=> new CartItemVM()
                {
                    CartItemId = cartItem.Id,
                    Name = cartItem.Product.Name,
                    ImageUrl = cartItem.Product.ImageUrl,
                    Price = (double)cartItem.Product.Price,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    
                }).ToList(),
            };

            return View(cartVm);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                _cartService.AddToCart(userId,productId,quantity);
                return RedirectToAction("Index", "Home");
            }
           
            return View();
        }
    }
}
