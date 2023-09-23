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
        // product/list => tüm ürünleri getirecek (bu yüzden int? id) id nullable olmalı yoksa 0 değeri gelir hata alırız
        //product/list /2 gibi id parametersi varsa o parametre categoryId sini denk gelicek ve o kategoriye ait ürünler listeleyeceğiz
        public IActionResult List(int? id,string queryString)
        {
            var products = ProductRepository.Products;

            if (id != null)
            {  // yani bir parameter gelmiştir CategoryId gelmiş bize
               products = ProductRepository.Products.Where(x => x.CategoryId == id).ToList(); 
            }
            // Search den Gelen QueryString'e göre ürünlerimizi filtreler
            if (!string.IsNullOrEmpty(queryString))
            {
                // products.Where dememizin sebebi yukarıda products listesinin Kategoriye göre filtrelenmesinden dolayı
                products = products.Where(p=>p.Name.ToLower().Contains(queryString.ToLower()) || p.Description.Contains(queryString) ).ToList();
            }

            var productVM = new ProductCategoriesVM()
            {
                Categories = CategoryRepository.Categories,
                Products = products
            };

            return View(productVM);
        }

        public IActionResult Details(int id)
        {
            var p = ProductRepository.GetProductById(id);
            return View(p);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories=new SelectList(CategoryRepository.Categories,"Id","Name");
            return View(new AddProductVM());
        }
        [HttpPost]
        public IActionResult AddProduct(AddProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                var product = new Product()
                {
                    Price = productVM.Price,
                    Name = productVM.Name,
                    Description = productVM.Description,
                    ImageUrl = productVM.ImageUrl,
                    CategoryId = productVM.CategoryId
                };

                ProductRepository.AddProduct(product);

                return RedirectToAction("List");
            }

            return View(productVM);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = ProductRepository.GetProductById(id);
            ViewBag.Categories = new SelectList(CategoryRepository.Categories, "Id", "Name");
            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product p)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.UpdateProduct(p);
                return RedirectToAction("List");
            }

            return View(p);
        }

        public IActionResult DeleteProduct(int? id)
        {
            if (id != null)
            {
                ProductRepository.DeleteProduct(id);
            }
            return RedirectToAction("List");
        }
    }
}
