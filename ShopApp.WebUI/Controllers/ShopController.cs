using BusinessLayer.Abstract;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService,ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }


        public IActionResult List(string category)
        {

            var productListVM = new ProductListVM()
            {
                Products = _productService.ListProductsByCategory(category)
            };

            return View(productListVM);
        }

        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            // burada product ile beraber Categorileri'de çektik
            Product p = _productService.GetProductDetails((int)id);

            if(p == null)
            {
                return NotFound();
            }

            var productDetailVM = new ProductDetailVM()
            {
                Product = p,
                Categories = p.Categories.Select(c=>c.Category).ToList()
            };
            return View(productDetailVM);
        }


    }
}
