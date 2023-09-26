using BusinessLayer.Abstract;
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
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }



        public IActionResult Index()
        {
            var productListVM = new ProductListVM()
            {
                Products = _productService.GetAll()
            };

            return View(productListVM);
        }

        public IActionResult Details()
        {

            return View();
        }
    }
}