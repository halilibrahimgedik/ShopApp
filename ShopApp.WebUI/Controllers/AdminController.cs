using BusinessLayer.Abstract;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
//using Newtonsoft.Json;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;
using System.Text.Json;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // ! Product işlemleri
        public IActionResult ListProducts()
        {
            var productVm = new ListProductsVM()
            {
                Products = _productService.GetAll() // onaylı onaysız tüm ürünler gelicek
            };

            return View(productVm);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductVM product)
        {
            var p = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                Url = product.Url,
            };

            _productService.Add(p);

            var msj = new AlertMessage()
            {
                AlertType= "success",
                Message=$"{p.Name} adlı ürün eklendi"
            };

            TempData["Message"] = JsonSerializer.Serialize(msj);

            return RedirectToAction("ListProducts");
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p = _productService.GetById((int)id);

            if (p == null)
            {
                return NotFound();
            }

            var productVM = new ProductVM()
            {
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Url = p.Url,
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductVM product)
        {

            var p = _productService.GetById(product.Id);

            if(p == null)
            {
                return NotFound();
            }

            p.Name = product.Name;
            p.Price = product.Price;
            p.ImageUrl = product.ImageUrl;
            p.Description = product.Description;
            p.Url = product.Url;

            _productService.Update(p);

            var msj = new AlertMessage()
            {
                AlertType = "warning",
                Message = $"{p.Name} adlı ürün Güncellendi"
            };

            TempData["Message"] = JsonSerializer.Serialize(msj);

            return RedirectToAction("ListProducts");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int? deleteId)
        {
            if (deleteId == null)
            {
                return NotFound();
            }
            var p = _productService.GetById((int)deleteId);
            if(p == null)
            {
                return NotFound();
            }
            _productService.Delete(p);

            var msj = new AlertMessage()
            {
                AlertType="danger",
                Message = $"{p.Name} adlı ürün silinmiştir"
            };

            TempData["Message"] = JsonSerializer.Serialize(msj);

            return RedirectToAction("ListProducts");
        }



        // ! Kategori işlemleri
        public IActionResult ListCategories()
        {
            var categories = _categoryService.GetAll();
            if(categories== null)
            {
                return NotFound();
            }

            var listCategories = new ListCategoriesVM() 
            {
                Categories = categories
            }; 
            return View(listCategories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryVM category)
        {
            
            var c = new Category()
            {
                Name= category.Name,
                Description= category.Description,
                Url= category.Url
            };

            _categoryService.Add(c);

            var msj = new AlertMessage()
            {
                AlertType = "success",
                Message = $"{c.Name} adlı Kategori eklenmiştir"
            };

            TempData["Message"]= JsonSerializer.Serialize(msj);

            return RedirectToAction("ListCategories");
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var c = _categoryService.GetById((int)id);

            if (c == null)
            {
                return NotFound();
            }

            //view sayfası categoryVM istiyor, Category gönderirsen hata alırsın
            var categoryVM = new CategoryVM()
            {
                Name= c.Name,
                Description= c.Description,
                Url= c.Url
            };

            ViewResult result = View(categoryVM);
            return result;
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryVM category)
        {
            var c = _categoryService.GetById(category.Id);

            if (c == null)
            {
                return NotFound();
            }
            else
            {
                c.Name = category.Name;
                c.Description = category.Description;
                c.Url = category.Url;

                _categoryService.Update(c);
            }

            var msj = new AlertMessage()
            {
                AlertType = "warning",
                Message = $"{c.Name} adlı Kategori Güncellendi"
            };

            TempData["Message"] = JsonSerializer.Serialize(msj);

            return RedirectToAction("ListCategories");

        }

        public IActionResult DeleteCategory(int? deleteId)
        {
            if (deleteId == null)
            {
                return NotFound();
            }

            var c = _categoryService.GetById((int)deleteId);

            if (c == null)
            {
                return NotFound();
            }

            _categoryService.Delete(c);

            var msj = new AlertMessage()
            {
                AlertType = "danger",
                Message = $"{c.Name} adlı Kategori silinmiştir"
            };

            TempData["Message"] = JsonSerializer.Serialize(msj);
            return RedirectToAction("ListCategories");
        }
    }
}
