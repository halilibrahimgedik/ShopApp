using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        public OrderController(IOrderService orderService,UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }


        public IActionResult GetOrders()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetOrders(userId);

            var orderListModel = new List<OrderListVM>();

            OrderListVM orderModel;
            foreach (var order in orders)
            {
                orderModel=new OrderListVM();

                orderModel.OrderId= order.OrderId;
                orderModel.UserId= userId;
                orderModel.OrderDate = order.OrderDate;
                orderModel.Phone = order.Phone;
                orderModel.Address = order.Address;
                orderModel.City = order.City;
                orderModel.Email = order.Email;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.OrderState = order.OrderState;
                orderModel.PaymentType = order.PaymentType;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.OrderId,
                    Name = i.Product.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.ImageUrl

                }).ToList();

                orderListModel.Add(orderModel);
            }
            return View("Orders", orderListModel);
        }
    }
}
