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

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        //! localhost/products/telefon?page=1  => page parametresini QueryString olarak alabiliriz route'a atamaya gerek yok
        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 3; // bir sayfada kaç ürün gösterilecek onu tanımlayalım
            var productListVM = new ListProductsVM()
            {
                PageInfo = new PageInfo() 
                {   //TODO KAtegoriye göre kaç ürün var ben onu almak istiyorum
                    TotalItems = _productService.GetCountByCategory(category),
                    
                    CurrentCategory = category,
                    CurrentPage = page,
                    ItemsPerPage = pageSize 
                },
                Products = _productService.ListProductsByCategory(category, page, pageSize)
            };

            return View(productListVM);
        }

        public ActionResult Details(string url)
        {
            if (url == null)
            {
                return NotFound();
            }
            // burada product ile beraber Categorileri'de çektik
            Product p = _productService.GetProductDetails(url);

            if (p == null)
            {
                return NotFound();
            }

            var productDetailVM = new ProductDetailVM()
            {
                Product = p,
                Categories = p.Categories.Select(c => c.Category).ToList()
            };
            return View(productDetailVM);
        }


    }
}
