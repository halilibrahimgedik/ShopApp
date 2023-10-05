using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;         //! Kullanıcak olduğumuz user sınıfını veriyoruz ve kullanıcı oluşturma temel login işlemlerini barındırıyor
        private SignInManager<User> sıgnInManager;     //! Cookie Olaylarını yönetiyor

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.sıgnInManager = signInManager;
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
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

            var result = await userManager.CreateAsync(user,register.Password);

            if(result.Succeeded)
            {
                //TODO Token Bilgisi oluşturulmalı burada ve mail ile gönderilmeli
                return RedirectToAction("Login");
            }

            // eğer hata farklı bir şeyden kaynaklanıyorsa ModelState içine hata mesajı ekleyebiliriz
            ModelState.AddModelError("","Bilinmeyen Bir hata meydana geldi, lütfen daha sonra tekrar deneyiniz");
            return View(register);
        }
    }
}
