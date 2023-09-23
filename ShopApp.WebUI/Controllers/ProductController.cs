using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class ProductController : Controller
    {


        public IActionResult List()
        {
            //List<Product> products = new()
            //{
            //    new Product {Name="Iphone 15",Price=55000,Description="çok iyi telefon", ImageUrl="/img/iphone-green.jpg",IsApproved=true},
            //    new Product {Name="Iphone 15",Price=53000,Description="çok iyi telefon", ImageUrl="/img/iphone-blue.jpg",IsApproved=true},
            //    new Product {Name="Iphone 15",Price=52000,Description="iyi telefon", ImageUrl="/img/iphone-black.jpg",IsApproved=true},
            //    new Product {Name="Iphone 15",Price=51000,Description="çok iyi telefon", ImageUrl="/img/iphone-pink.jpg",IsApproved=true},
            //    new Product {Name="Iphone 15",Price=52000,Description="çok iyi telefon", ImageUrl="/img/iphone-yellow.jpg",IsApproved=true}
            //};

            //var categories = new List<Category>()
            //{
            //    new(){Name = "Telefonlar",Description = "Telefon Kategorisi"},
            //    new(){Name = "Bilgisayar",Description = "Telefon Kategorisi"},
            //    new(){Name = "Elektronik",Description = "Telefon Kategorisi"},
            //};
            
            var productVM = new ProductCategoriesVM()
            {
                Categories = CategoryRepository.Categories,
                Products = ProductRepository.Products,
            };

            return View(productVM);
        }
    }
}
