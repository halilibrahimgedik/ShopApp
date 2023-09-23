using DataAccessLayer.Concrete;
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
           

            var productCategoriesVM = new ProductCategoriesVM()
            {
                Products = ProductRepository.Products
            };

            return View(productCategoriesVM);

        }

        public IActionResult Details()
        {


            return View();
        }


    }
}