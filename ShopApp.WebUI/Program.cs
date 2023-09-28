using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using shopapp.data.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injection
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
