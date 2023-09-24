using DataAccessLayer.Abstract;
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
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        public IActionResult Index()
        {
            var productCategoriesVM = new ProductCategoriesVM()
            {
                Products = _productRepository.GetAll()
            };

            return View(productCategoriesVM);
        }

        public IActionResult Details()
        {

            return View();
        }
    }
}