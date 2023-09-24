using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }


        // product/list => tüm ürünleri getirecek (bu yüzden int? id) id nullable olmalı yoksa 0 değeri gelir hata alırız
        //product/list /2 gibi id parametersi varsa o parametre categoryId sini denk gelicek ve o kategoriye ait ürünler listeleyeceğiz
        public IActionResult List(int? id, string queryString)
        {
            var products = _productService.GetAll();

            if (id != null)
            {  // yani bir parameter gelmiştir CategoryId gelmiş bize
                //products = _productService.GetAll().Where(x => x.CategoryId == id).ToList();
            }
            // Search den Gelen QueryString'e göre ürünlerimizi filtreler
            if (!string.IsNullOrEmpty(queryString))
            {
                // products.Where dememizin sebebi yukarıda products listesinin Kategoriye göre filtrelenmesinden dolayı
                products = products.Where(p => p.Name.ToLower().Contains(queryString.ToLower()) || p.Description.Contains(queryString)).ToList();
            }

            var productVM = new ProductCategoriesVM()
            {
                Categories = _categoryService.GetAll(),
                Products = products
            };

            return View(productVM);
        }

        public IActionResult Details(int id)
        {
            var p = _productService.GetById(id);
            return View(p);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View(new AddProductVM());
        }
        [HttpPost]
        public IActionResult AddProduct(AddProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Price = productVM.Price,
                    Name = productVM.Name,
                    Description = productVM.Description,
                    ImageUrl = productVM.ImageUrl,

                    //CategoryId = productVM.CategoryId
                };

                _productService.Add(product);

                return RedirectToAction("List");
            }

            return View(productVM);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _productService.GetById(id);
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product p)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(p);
                return RedirectToAction("List");
            }

            return View(p);
        }

        public IActionResult DeleteProduct(int? id)
        {
            if (id != null)
            {
                var p = _productService.GetById(id);
                _productService.Delete(p);
            }
            return RedirectToAction("List");
        }
    }
}
