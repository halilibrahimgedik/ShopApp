using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string MyAllowOrigins = "_myAllowOrigins";
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowOrigins,
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

builder.Services.AddDbContext<ShopAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
//! unitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
