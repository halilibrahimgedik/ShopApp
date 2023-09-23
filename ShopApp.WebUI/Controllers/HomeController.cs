using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;
using System.Diagnostics;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = new List<Product>()
            {
                new Product {Name="Iphone 15",Price=55000,Description="çok iyi telefon", ImageUrl="img/iphone-green.jpg",IsApproved=true},
                new Product {Name="Iphone 15",Price=53000,Description="çok iyi telefon", ImageUrl="img/iphone-blue.jpg",IsApproved=true},
                new Product {Name="Iphone 15",Price=52000,Description="iyi telefon", ImageUrl="img/iphone-black.jpg",IsApproved=true},
                new Product {Name="Iphone 15",Price=51000,Description="çok iyi telefon", ImageUrl="img/iphone-pink.jpg",IsApproved=true},
                new Product {Name="Iphone 15",Price=52000,Description="çok iyi telefon", ImageUrl="img/iphone-yellow.jpg",IsApproved=true}
            };

            var productCategoriesVM = new ProductCategoriesVM()
            {
                Products = products
            };

            return View(productCategoriesVM);

        }

        public IActionResult Details()
        {


            return View();
        }


    }
}