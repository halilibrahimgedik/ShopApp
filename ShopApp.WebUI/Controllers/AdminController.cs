﻿using BusinessLayer.Abstract;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Win32;
using ShopApp.WebUI.EmailServices;
using ShopApp.WebUI.Identity;
//using Newtonsoft.Json;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.Validators;
using ShopApp.WebUI.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ShopApp.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(IProductService productService, ICategoryService categoryService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IEmailSender emailSender)
        {
            _productService = productService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
            _emailSender = emailSender;

        }


        // ! Product işlemleri
        public async Task<IActionResult> ListProducts()
        {
            var products = await _productService.GetAll();
            var productVm = new ListProductsVM()
            {
                Products = products // onaylı onaysız tüm ürünler gelicek
            };

            return View(productVm);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.AllCategories =  await _categoryService.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductVM product, int[] categoryIds, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine(ModelState.ErrorCount);
                ViewBag.AllCategories = await _categoryService.GetAll();
                return View(product);
            }

            try
            {
                var p = new Product()
                {
                    Name = product.Name!,
                    Price = product.Price!,
                    Description = product.Description!,
                    Url = product.Url!,
                    IsApproved = product.IsApproved,
                    IsHome = product.IsHome,
                    AddedDate = product.AddedDate,
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
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                CreateMessage("danger", " Ürün Eklenirken Bir hata meydana geldi");
            }
            

            return RedirectToAction("ListProducts");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
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

            var updateProductVM = new UpdateProductVM()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Url = p.Url,
                IsApproved = p.IsApproved,
                IsHome = p.IsHome,
                SelectedCategories = p.Categories.Select(pc => pc.Category).ToList() // ürüne ait kategriler
            };

            ViewBag.AllCategories = await _categoryService.GetAll(); // tüm kategoriler
            return View(updateProductVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(UpdateProductVM product, int[] categoryIds, IFormFile file)
        {

            var p = await _productService.GetById(product.Id);

            if (p == null)
            {
                return NotFound();
            }

            ViewBag.UploadedFile = file;

            if (!ModelState.IsValid)
            {
                ViewBag.AllCategories = await _categoryService.GetAll();
                return View(product);
            }

            try
            {
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

                p.Name = product.Name!;
                p.Price = product.Price;
                p.Description = product.Description!;
                p.Url = product.Url!;
                p.IsApproved = product.IsApproved;
                p.IsHome = product.IsHome;

                // ? Gelen checkbox daki kategori bilgilerini categoryIds sayesinde taşıyoruz ve update metodu ile vt aktarıyoruz
                _productService.Update(p, categoryIds);

                CreateMessage("warning", $"{p.Name} adlı ürün Güncellendi");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CreateMessage("danger", "Ürün GÜncellenirken Bir hata meydana geldi");
            }
           
            return RedirectToAction("ListProducts");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int? deleteId)
        {
            if (deleteId == null)
            {
                return NotFound();
            }
            var p = await _productService.GetById((int)deleteId);
            if (p == null)
            {
                return NotFound();
            }
            _productService.Delete(p);

            CreateMessage("danger", $"{p.Name} adlı ürün silinmiştir");

            return RedirectToAction("ListProducts");
        }


        // ! Kategori işlemleri
        public async Task<IActionResult> ListCategories()
        {
            var categories = await _categoryService.GetAll();
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
            if (!ModelState.IsValid)
            {
                return View(category);
            }

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
        public async Task<IActionResult> EditCategory(CategoryVM category)
        {
            var c = await _categoryService.GetById(category.Id);

            if (c == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                c.Name = category.Name;
                c.Description = category.Description;
                c.Url = category.Url;

                _categoryService.Update(c);

                CreateMessage("warning", $"{c.Name} adlı Kategori Güncellendi");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                CreateMessage("danger", "Kategori güncellenirken bİr Hata Meydana Geldi");
            }

            return RedirectToAction("ListCategories");
        }

        public async Task<IActionResult> DeleteCategory(int? deleteId)
        {
            if (deleteId == null)
            {
                return NotFound();
            }

            var c = await _categoryService.GetById((int)deleteId);

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

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var list  = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;

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

                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
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

        public async Task<IActionResult> DeleteRole(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                CreateMessage("danger", "Silmeye Çalıştığınız Rolün Id bilgisi Hatalı ");
                return RedirectToAction("ListRoles");
            }

            try
            {
                IdentityRole role = await _roleManager.FindByIdAsync(roleId);
                await _roleManager.DeleteAsync(role);
            }
            catch (Exception e)
            {
                CreateMessage("danger", "bir hata meydana geldi, Rol silinemedi.");
                Console.WriteLine(e.Message);
            }

            return RedirectToAction("ListRoles");
        }


        //! User İşlemleri
        public IActionResult ListUsers()
        {
            return View(_userManager.Users.ToList());
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            // girilen bilgiler validation kurallarını geçtiyse user oluşturmalıyız
            var user = new User()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.UserName,
            };

            // mail adresi önceden eklenmiş mi kontrol edelim
            var isMailAdressUsedBefore = await _userManager.FindByEmailAsync(user.Email);

            if (isMailAdressUsedBefore != null)
            {
                CreateMessage("warning", "Bu Mail adresi ile önceden bir hesap oluşturulmuştur. Lütfen başka bir mail adresi giriniz.");
                return View(register);
            }

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                // oluşan user hesabını direkt "customer" rolüne atayalım.
                //await _userManager.AddToRoleAsync(user, "customer");

                //TODO Token Bilgisi oluşturulmalı burada ve mail ile gönderilmeli
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user); // bizden bir user bilgisi alarak token oluşturuyor,oluşturulan token veri tabanına kaydediliyor ve daha sonra token bilgisi ile onaylama yapacağız.

                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = token
                });
                //TODO Url oluşturulduktan sonra bu url kullanıcıya mail ile gönderilmeli

                await _emailSender.SendEmailAsync(register.Email, "Hesabınızı Onaylayınız", $"Lütfen ShopApp uygulamasından size gönderilen <a href='https://localhost:7037{url}'>linke tıklayarak </a> hesabınızı onaylayınız.");

                return RedirectToAction("ListUsers");
            }

            // eğer hata farklı bir şeyden kaynaklanıyorsa ModelState içine hata mesajı ekleyebiliriz yada alertBox ile de yapabiliriz
            ModelState.AddModelError("", "Bilinmeyen Bir hata meydana geldi, lütfen daha sonra tekrar deneyiniz");
            return View(register);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            // ilgili user'a ait role bilgilerini bize döndüren metod
            var userRoles = await _userManager.GetRolesAsync(user);

            //tüm rolleri çekelim
            var roles = _roleManager.Roles.Select(role=>role.Name);
            ViewBag.Roles = roles;

            if (user != null)
            {
                var model = new UserDetailVM()
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    UserRoles = userRoles,
                };

                return View(model);
            }

            return Redirect("/admin/user/list");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserDetailVM model, string[] selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                CreateMessage("danger", "Validation hatasından dolayı user bilgileri güncellenemedi");
                var roles = _roleManager.Roles.Select(role => role.Name);
                ViewBag.Roles = roles;

                var user2 = await _userManager.FindByIdAsync(model.UserId);
                var userRoles = await _userManager.GetRolesAsync(user2);

                model.UserRoles = userRoles;
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user != null)
            {
                user.FirstName = model.Firstname;
                user.LastName = model.Lastname;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailConfirmed;
                user.UserName = model.Username;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    // eğer kullanıcı hiç rol seçmez ise null referance hatası almayalım
                    selectedRoles = selectedRoles?? new string[] { };

                    await _userManager.AddToRolesAsync(user,selectedRoles.Except(userRoles).ToArray());
                    await _userManager.RemoveFromRolesAsync(user,userRoles.Except(selectedRoles).ToArray());

                    return Redirect("/admin/user/list/");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                CreateMessage("warning", "Silmek İstediğiniz user bulunamadı");
                return RedirectToAction("ListUsers");
            }

            try
            {
                var user = await _userManager.FindByIdAsync(UserId);
                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                CreateMessage("danger", "Bilinmeyen bir hata oluştu");
                Console.WriteLine(ex.Message);
            }
            
            return RedirectToAction("ListUsers");
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
