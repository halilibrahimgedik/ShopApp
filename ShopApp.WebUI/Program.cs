using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EfCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ShopApp.WebUI.EmailServices;
using ShopApp.WebUI.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ShopAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

// Toplu FLUENT VALIDATION
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<Program>();

builder.Services.Configure<IdentityOptions>(options =>
{
    //!Password
    options.Password.RequireDigit = true;               // þifrede rakam olmalý
    options.Password.RequireLowercase = true;           // þifrede Küçük harf olmalý
    options.Password.RequireUppercase = true;           // þifrede Büyük harf olmalý  
    options.Password.RequiredLength = 6;                // þifre en az 6 karakter uzunluðunda olmalý
    options.Password.RequireNonAlphanumeric = false;    // þifrede ( ., +, -, ~, @,<,>,*,/ gibi iþaretler zorunlu olarak olmalý mý?)

    //!Lockout (kullanýcýnýn hesabýnýn kilitlenip kilitlenmemesi ayarý)
    options.Lockout.MaxFailedAccessAttempts = 5;                        // Kullancý Yanlýþ bir parolayý max 5 kere girebilir ve sonra hesap kilitlenir. 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);   // 5 dakika boyunca hesap kilitlenecek
    options.Lockout.AllowedForNewUsers = true;

    //options.User.AllowedUserNameCharacters = "";                  // Username içerisinde olmasýný istediðimiz karakterler
    options.User.RequireUniqueEmail = true;                 //? ayný Mail adresi ile kayýt yapan 2 kullanýcý olamaz
    options.SignIn.RequireConfirmedEmail = true;           //? Kulanýcý üye olduktan sonra MUTLAKA hesabýný  Onaylamalý 
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
        Name = ".ShopApp.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
//! unitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i => new SmtpEmailSender(
                                                                builder.Configuration["EmailSender:Host"],
                                                                builder.Configuration.GetValue<int>("EmailSender:Port"),
                                                                builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                                                                builder.Configuration["EmailSender:UserName"],
                                                                builder.Configuration["EmailSender:Password"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
    name: "orders",
    pattern: "orders",
    defaults: new { controller = "Order", action = "GetOrders" }
    );

//? Cart Route settings 
app.MapControllerRoute(
    name: "cart",
    pattern: "mycart",
    defaults: new { controller = "Cart", action = "ShowCart" }
    );

app.MapControllerRoute(
    name: "checkout",
    pattern: "checkout",
    defaults: new { controller = "Cart", action = "Checkout" }
    );

//? Admin User Route settings
app.MapControllerRoute(
    name: "adminusers",
    pattern: "admin/user/list",
    defaults: new { controller = "Admin", action = "ListUsers" });

app.MapControllerRoute(
    name: "adminusercreate",
    pattern: "admin/user/create",
    defaults: new { controller = "Admin", action = "CreateUser" });

app.MapControllerRoute(
    name: "adminrolecreate",
    pattern: "admin/user/edit/{userId?}",
    defaults: new { controller = "Admin", action = "EditUser" });

app.MapControllerRoute(
    name: "adminuserdelete",
    pattern: "admin/user/delete",
    defaults: new { controller = "Admin", action = "DeleteUser" });

//? Admin Role Route settings
app.MapControllerRoute(
    name: "adminroles",
    pattern: "admin/role/list",
    defaults: new { controller = "Admin", action = "ListRoles" });

app.MapControllerRoute(
    name: "adminrolecreate",
    pattern: "admin/role/create",
    defaults: new { controller = "Admin", action = "CreateRole" });

app.MapControllerRoute(
    name: "adminrolecreate",
    pattern: "admin/role/edit/{id?}",
    defaults: new { controller = "Admin", action = "EditRole" });

//? Admin Category Route settings
app.MapControllerRoute(
    name: "admincategorieslist",
    pattern: "admin/categories",
    defaults: new { controller = "Admin", action = "ListCategories" });

app.MapControllerRoute(
    name: "admincategorycreate",
    pattern: "admin/categories/create",
    defaults: new { controller = "Admin", action = "CreateCategory" });

app.MapControllerRoute(
    name: "adminproductedit",
    pattern: "admin/categories/{id?}",
    defaults: new { controller = "admin", action = "EditCategory" });

//? Admin Product Route settings
app.MapControllerRoute(
    name: "adminproductslist",
    pattern: "admin/products",
    defaults: new { controller = "Admin", action = "ListProducts" });

app.MapControllerRoute(
    name: "adminproductcreate",
    pattern: "admin/products/create",
    defaults: new { controller = "Admin", action = "CreateProduct" });

app.MapControllerRoute(
    name: "adminproductedit",
    pattern: "admin/products/{id?}",
    defaults: new { controller = "admin", action = "EditProduct" });


//? User Pages Route settings
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


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();

    await SeedIdentity.Seed(userManager, roleManager,cartService, app.Configuration);
}

app.Run();
