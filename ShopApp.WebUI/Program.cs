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
    options.Password.RequireDigit = true;       // þifrede rakam olmalý
    options.Password.RequireLowercase = true;   // þifrede Küçük harf olmalý
    options.Password.RequireUppercase = true;   // þifrede Büyük harf olmalý  
    options.Password.RequiredLength = 6;        // þifre en az 6 karakter uzunluðunda olmalý

    //!Lockout (kullanýcýnýn hesabýnýn kilitlenip kilitlenmemesi ayarý)
    options.Lockout.MaxFailedAccessAttempts = 5;                        // Kullancý Yanlýþ bir parolayý max 5 kere girebilir ve sonra hesap kilitlenir. 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);   // 5 dakika boyunca hesap kilitlenecek
    options.Lockout.AllowedForNewUsers = true;

    // options.User.AllowedUserNameCharacters = "";                 // Username içerisinde olmasýný istediðimiz karakterler
    options.User.RequireUniqueEmail = true;                 //? ayný Mail adresi ile kayýt yapan 2 kullanýcý olamaz
    options.SignIn.RequireConfirmedEmail = false;           //? Kulanýcý üye olduktan sonra MUTLAKA hesabýný  Onaylamalý 
    options.SignIn.RequireConfirmedPhoneNumber = false;     //? Telefon için onay istiyorsak kullanabiliriz
});

//TODO Cookie Ayarlarý
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";

    options.SlidingExpiration = true;       //? varsayýlan cookie süresi 20 dakikadýr ve her istekde sýfýrlanýr. (false dersek , ne kadar istek atarsak atalým cookie süremiz hep 20 dakikadýr. 20 dakika dolunca login olmalýyýz tekrar)
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  //? VarsayýlanCookie süresini ayarlama
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,    //? Cookie bilgisi tarayýcýdan sadece bir http isteði ile alýnabilsin, scriptler alamasýn
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
    //!Static sýnýf ile Data Seeding
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


// TODO Kullanýcý Route'larý
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
