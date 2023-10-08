using BusinessLayer.Abstract;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ShopApp.WebUI.Identity;
//using Newtonsoft.Json;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;
using System.Text.Json;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(IProductService productService, ICategoryService categoryService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
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
            ViewBag.AllCategories = _categoryService.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductVM product, int[] categoryIds, IFormFile file)
        {
            var p = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Url = product.Url,
                IsApproved = product.IsApproved,
                IsHome = product.IsHome
            };

            if (file != null)
            {
                var imageExtension = Path.GetExtension(file.FileName);

                var randomName = string.Format($"{Guid.NewGuid()}{imageExtension}");

                p.ImageUrl = randomName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            _productService.Add(p, categoryIds);

            CreateMessage("success", $"{p.Name} adlı ürün eklendi");

            return RedirectToAction("ListProducts");
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p = _productService.GetProductByIdWithCategories((int)id);

            if (p == null)
            {
                return NotFound();
            }

            var productVM = new ProductVM()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Url = p.Url,
                SelectedCategories = p.Categories.Select(pc => pc.Category).ToList() // ürüne ait kategriler
            };

            ViewBag.AllCategories = _categoryService.GetAll(); // tüm kategoriler
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductVM product, int[] categoryIds, IFormFile file)
        {

            var p = _productService.GetById(product.Id);

            if (p == null)
            {
                return NotFound();
            }

            p.Name = product.Name;
            p.Price = product.Price;
            p.Description = product.Description;
            p.Url = product.Url;

            if (file != null)
            {
                var imageExtension = Path.GetExtension(file.FileName);

                var randomName = string.Format($"{Guid.NewGuid()}{imageExtension}");

                p.ImageUrl = randomName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            // ? Gelen checkbox daki kategori bilgilerini categoryIds sayesinde taşıyoruz ve update metodu ile vt aktarıyoruz
            _productService.Update(p, categoryIds);

            CreateMessage("warning", $"{p.Name} adlı ürün Güncellendi");

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
            if (p == null)
            {
                return NotFound();
            }
            _productService.Delete(p);

            CreateMessage("danger", $"{p.Name} adlı ürün silinmiştir");

            return RedirectToAction("ListProducts");
        }



        // ! Kategori işlemleri
        public IActionResult ListCategories()
        {
            var categories = _categoryService.GetAll();
            if (categories == null)
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
                Name = category.Name,
                Description = category.Description,
                Url = category.Url
            };

            _categoryService.Add(c);

            CreateMessage("success", $"{c.Name} adlı Kategori eklenmiştir");

            return RedirectToAction("ListCategories");
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var c = _categoryService.GetByIdWithProducts((int)id);

            if (c == null)
            {
                return NotFound();
            }

            //view sayfası categoryVM istiyor, Category gönderirsen hata alırsın
            var categoryVM = new CategoryVM()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Url = c.Url,
                Products = c.Products.Select(pc => pc.Product).ToList()
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

            CreateMessage("warning", $"{c.Name} adlı Kategori Güncellendi");

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

            CreateMessage("danger", $"{c.Name} adlı Kategori silinmiştir");

            return RedirectToAction("ListCategories");
        }

        public IActionResult DeleteProductFromCategories(int productId, int categoryId)
        {
            _categoryService.DeleteProductFromCategories(productId, categoryId);

            //return RedirectToAction("/admin/categories/" + categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }



        // ! Role işlemleri
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles.ToList();

            return View(roles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));

            if (result.Succeeded)
            {
                CreateMessage("success", "Yeni role Eklenmiştir.");

                return RedirectToAction("ListRoles");
            }

            //!  Eğer role oluşturulamadıysa error vermeliyiz => bu kullanımı da bil diye bunu yapıyoruz
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonMembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers; 
                // listlerin referans tipli olmasını kullanabiliriz burda, true dönerse list => stack de members 'ı işaret edicek ve ekleme yaparken(list.Add(user) derken) members 'a eklemiş olacağız, false dönerse list stackde nonMembers'ı işaret edicek ve list.Add(user) dediğimizde bu sefer nonMembers'a user eklenecek
                list.Add(user);
            }

            var model = new RoleDetails()
            {
                Role = role,
                NonMembers = nonMembers,
                Members = members 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                // Role Eklenecek UserId'ler
                foreach (var userId in model.IdsToAdd)
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if(user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                    }                   
                }

                // Role'den silinecek UserId'ler
                foreach (var userId in model.IdsToDelete)
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                CreateMessage("success", "işlemleriniz başarıyla gerçekleştirilmiştir");
            }

            return Redirect("/admin/role/edit/" + model.RoleId);
        }



        private void CreateMessage(string alertType, string message)
        {
            var msj = new AlertMessage()
            {
                AlertType = alertType,
                Message = message
            };
            TempData["Message"] = JsonSerializer.Serialize(msj);
        }
    }
}
