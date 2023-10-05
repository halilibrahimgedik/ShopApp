using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Concrete.EfCore;
using ShopApp.WebUI.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(@"Data Source=Shop.db"));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    //!Password
    options.Password.RequireDigit = true;       // �ifrede rakam olmal�
    options.Password.RequireLowercase = true;   // �ifrede K���k harf olmal�
    options.Password.RequireUppercase = true;   // �ifrede B�y�k harf olmal�  
    options.Password.RequiredLength = 6;        // �ifre en az 6 karakter uzunlu�unda olmal�

    //!Lockout (kullan�c�n�n hesab�n�n kilitlenip kilitlenmemesi ayar�)
    options.Lockout.MaxFailedAccessAttempts = 5;                        // Kullanc� Yanl�� bir parolay� max 5 kere girebilir ve sonra hesap kilitlenir. 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);   // 5 dakika boyunca hesap kilitlenecek
    options.Lockout.AllowedForNewUsers = true;

    // options.User.AllowedUserNameCharacters = "";                 // Username i�erisinde olmas�n� istedi�imiz karakterler
    options.User.RequireUniqueEmail = true;                 //? ayn� Mail adresi ile kay�t yapan 2 kullan�c� olamaz
    options.SignIn.RequireConfirmedEmail = false;           //? Kulan�c� �ye olduktan sonra MUTLAKA hesab�n�  Onaylamal� 
    options.SignIn.RequireConfirmedPhoneNumber = false;     //? Telefon i�in onay istiyorsak kullanabiliriz
});

//TODO Cookie Ayarlar�
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";

    options.SlidingExpiration = true;       //? varsay�lan cookie s�resi 20 dakikad�r ve her istekde s�f�rlan�r. (false dersek , ne kadar istek atarsak atal�m cookie s�remiz hep 20 dakikad�r. 20 dakika dolunca login olmal�y�z tekrar)
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  //? Varsay�lanCookie s�resini ayarlama
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,    //? Cookie bilgisi taray�c�dan sadece bir http iste�i ile al�nabilsin, scriptler alamas�n
        Name = ".ShopApp.Security.Cookie"
    };
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>();

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //!Static s�n�f ile Data Seeding
    //SeedDatabase.Seed();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name:"admincategorieslist",
    pattern:"admin/categories",
    defaults: new {controller="Admin",action="ListCategories"});

app.MapControllerRoute(
    name: "admincategorycreate",
    pattern: "admin/categories/create",
    defaults: new { controller = "Admin", action = "CreateCategory" });

app.MapControllerRoute(
    name: "adminproductedit",
    pattern: "admin/categories/{id?}",
    defaults: new { controller = "admin", action = "EditCategory" });


app.MapControllerRoute(
    name:"adminproductslist",
    pattern:"admin/products",
    defaults: new { controller = "Admin", action= "ListProducts" });

app.MapControllerRoute(
    name: "adminproductcreate",
    pattern: "admin/products/create",
    defaults: new { controller = "Admin", action = "CreateProduct" });

app.MapControllerRoute(
    name: "adminproductedit",
    pattern: "admin/products/{id?}",
    defaults: new { controller = "admin", action = "EditProduct" });


// TODO Kullan�c� Route'lar�
app.MapControllerRoute(
    name: "search",
    pattern: "search",
    defaults: new { controller = "Home", action = "SearchProduct" });

app.MapControllerRoute(
    name: "products",
    pattern: "products/{category?}", //
    defaults: new { controller = "Shop", action = "List" });

app.MapControllerRoute(
    name: "productdetails",
    pattern: "{url}", //
    defaults: new { controller = "Shop", action = "details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
