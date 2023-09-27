using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        public AdminController(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult ProductList()
        {
            var productVm = new ProductListVM()
            {
                Products = _productService.GetAll() // onaylı onaysız tüm ürünler gelicek
            };

            return View(productVm);
        }
    }
}
