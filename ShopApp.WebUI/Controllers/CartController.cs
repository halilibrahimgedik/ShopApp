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
using EntityLayer;
using ShopApp.WebUI.Models;
using System.Text.Json;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, UserManager<User> userManager, IOrderService orderService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;

        }

        public IActionResult ShowCart()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var cartVm = new CartVM()
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

            return View(cartVm);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity, string? returnUrl)
        {
            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                _cartService.AddToCart(userId, productId, quantity);
                return Redirect("/products");
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteCartItemFromCart(int cartItemId)
        {
            var userId = _userManager.GetUserId(User);

            _cartService.DeleteCartItemFromCart(userId, cartItemId);
            return RedirectToAction("ShowCart");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            OrderVM orderVm = new OrderVM();

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
        public IActionResult Checkout(OrderVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartService.GetCartByUserId(userId);

                model.CartModel = new CartVM()
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

                var payment = PaymentProcess(model);

                if (payment.Status == "success")
                {
                    SaveOrder(model, payment, userId);
                    ClearCart(model.CartModel.CartId);
                    return View("success");
                }
                else
                {
                    CreateMessage("danger", $"{payment.ErrorMessage}");
                }
            }

            return View(model);
        }

        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private void SaveOrder(OrderVM model, Payment payment, string userId)
        {
            var order = new Order();

            order.OrderNumber = new Random().Next(111111, 999999).ToString();

            order.OrderState = EnumOrderState.completed;
            order.PaymentType = EnumPaymentType.CreditCard;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderDate = new DateTime();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.UserId = userId;
            order.Address = model.Address;
            order.Phone = model.Phone;
            order.City = model.City;
            order.Email = model.Email;
            order.Note = model.Note;
            order.OrderItems = new List<EntityLayer.OrderItem>();

            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new EntityLayer.OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                };
                order.OrderItems.Add(orderItem);
            }

            _orderService.Create(order);
        }

        private Payment PaymentProcess(OrderVM model)
        {
            Iyzipay.Options options = new Iyzipay.Options();
            options.ApiKey = "sandbox-pG1IXNky5JEfu5jamGt3stAnSfiSegv8";
            options.SecretKey = "sandbox-xvnnT156quyRwGlCAdmsnUXwblcbAAoz";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111, 999999999).ToString();
            request.Price = model.CartModel.TotalPrice().ToString();
            request.PaidPrice = model.CartModel.TotalPrice().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //  paymentCard.CardNumber = "5528790000000008";
            // paymentCard.ExpireMonth = "12";
            // paymentCard.ExpireYear = "2030";
            // paymentCard.Cvc = "123";

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = model.FirstName;
            buyer.Surname = model.LastName;
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
            BasketItem basketItem;

            foreach (var item in model.CartModel.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "Telefon";
                basketItem.Price = (item.Price * item.Quantity).ToString();
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItems.Add(basketItem);
            }
            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }

        private void CreateMessage(string alertType, string message)
        {
            var msj = new AlertMessage()
            {
                AlertType = alertType,
                Message = message
            };
            TempData["Message"] = JsonSerializer.Serialize(msj);
        }
    }
}