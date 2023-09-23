using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // product/list => tüm ürünleri getirecek (bu yüzden int? id) id nullable olmalı yoksa 0 değeri gelir hata alırız
        //product/list /2 gibi id parametersi varsa o parametre categoryId sini denk gelicek ve o kategoriye ait ürünler listeleyeceğiz
        public IActionResult List(int? id)
        {
            var products = ProductRepository.Products;

            if (id != null)
            {  // yani bir parameter gelmiştir CategoryId gelmiş bize
               products = ProductRepository.Products.Where(x => x.CategoryId == id).ToList(); 
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

    }
}
