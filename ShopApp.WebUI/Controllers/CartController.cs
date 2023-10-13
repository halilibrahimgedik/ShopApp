using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models.ViewModels;
using System.Net;
using System;
using Iyzipay.Request;
using Iyzipay.Model;


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
        public IActionResult AddToCart(int productId, int quantity, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                _cartService.AddToCart(userId,productId,quantity);
                return Redirect("/products");
            }
           
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCartItemFromCart(int cartItemId)
        {
            var userId = _userManager.GetUserId(User);

            _cartService.DeleteCartItemFromCart(userId,cartItemId);
            return RedirectToAction("ShowCart");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            OrderVM orderVm= new OrderVM();

            orderVm.CartModel = new CartVM()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(cartItem => new CartItemVM()
                {
                    CartItemId = cartItem.Id,
                    Name = cartItem.Product.Name,
                    ImageUrl = cartItem.Product.ImageUrl,
                    Price = (double)cartItem.Product.Price,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,

                }).ToList(),
            };
            return View(orderVm);
        }

        [HttpPost]
        public IActionResult Checkout(string cartId)
        {
            Iyzipay.Options options = new Iyzipay.Options();
            options.ApiKey = "sandbox-pG1IXNky5JEfu5jamGt3stAnSfiSegv8";
            options.SecretKey = "sandbox-xvnnT156quyRwGlCAdmsnUXwblcbAAoz";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "Halil Gedx";
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "halil";
            buyer.Surname = "gedik";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);

            if (payment.Status == "success")
            {
                return View("success");
            }
            return View();
        }
    }
}
