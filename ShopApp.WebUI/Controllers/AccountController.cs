using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.EmailServices;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;
using System.Text.Json;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager; //! Kullanıcak olduğumuz user sınıfını veriyoruz ve kullanıcı oluşturma temel login işlemlerini barındırıyor
        private readonly SignInManager<User> _signInManager;     //! Cookie Olaylarını yönetiyor
        private readonly IEmailSender _emailSender;              //! Mail gönderme işlemlerimizi yapacağız

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }


        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var loginVm = new LoginVM()
            {
                ReturnUrl = returnUrl,
            };
            return View(loginVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            //! 1-) eğer girilen email adresine göre bir user yoksa
            if (user == null)
            {
                ModelState.AddModelError("", "Bu Email adresine kayıtlı bir kullanıcı yoktur");
                return View(model);
            }

            //! eğer user varsa tarayıcıya bir cookie bırakmalıyız (AMA KULLANICINI HESABINI ONAYLAMIŞ OLMASI GEREKİYOR Cookie Bırakmamız için) 

            if (!await _userManager.IsEmailConfirmedAsync(user)) // user'ın e postası doğrulanmış mı ?
            {
                ModelState.AddModelError("", "Lütfen giriş yapmadan önce Mail Hesabınızı gönderilen link ile hesabınızı onaylayın.");
                return View(model);
            }

            //! Kullanıcın hesabı Onaylı, Şimdi Cokie Bırakalım 
            //? 3. parametre isPersistent alanı => program.cs de yazdığımız varsayılan cookie süresi ayarı mı kullanılsın ?, false => tarayıcı kapandığında cookie silinir
            //? 4. parametre ise "lockoutOnFailure" => program.cs de yazdığımız lockout ayarlarında x kere başarısız şifre girişinde hesabın kilitlenmesi ayarı açılsın mı, yapılandırmasını kullanmamızı sağlar.
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
                //return RedirectToAction("Index","Home");
            }

            ModelState.AddModelError("", "Email adresiniz veya şifreniz hatalıdır.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            // mail adresi önceden eklenmiş mi kontrol edelim
            var isMailAdressUsedBefore= await _userManager.FindByEmailAsync(user.Email);

            if (isMailAdressUsedBefore != null)
            {
                CreateMessage("warning", "Bu Mail adresi ile önceden bir hesap oluşturulmuştur. Lütfen başka bir mail adresi giriniz.");
            }

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                //TODO Token Bilgisi oluşturulmalı burada ve mail ile gönderilmeli
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user); // bizden bir user bilgisi alarak token oluşturuyor,oluşturulan token veri tabanına kaydediliyor ve daha sonra token bilgisi ile onaylama yapacağız.

                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = token
                });
                //TODO Url oluşturulduktan sonra bu url kullanıcıya mail ile gönderilmeli

                await _emailSender.SendEmailAsync(register.Email, "Hesabınızı Onaylayınız", $"Lütfen ShopApp uygulamasından size gönderilen <a href='https://localhost:7037{url}'>linke tıklayarak </a> hesabınızı onaylayınız.");

                return RedirectToAction("Login");
            }

            // eğer hata farklı bir şeyden kaynaklanıyorsa ModelState içine hata mesajı ekleyebiliriz yada alertBox ile de yapabiliriz
            ModelState.AddModelError("", "Bilinmeyen Bir hata meydana geldi, lütfen daha sonra tekrar deneyiniz");
            return View(register);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                CreateMessage("danger", "Geçersiz Token.");
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);

            // kullanıcı varsa
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    CreateMessage("success", "Hesabınız Başarıyla Onaylanmıştır");
                    return View();
                }
            }
            // kullanıcı yoksa
            CreateMessage("danger", "Hesabınız Onaylanmadı.");

            return View();
        }

        // AlertBox oluşturma metodu
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
