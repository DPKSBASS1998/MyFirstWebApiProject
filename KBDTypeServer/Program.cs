using KBDTypeServer.Application;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;
using KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
using KBDTypeServer.Infrastructure.Repositories.ProductRepositories;
using KBDTypeServer.Infrastructure.Repositories.UserRepositories;
using KBDTypeServer.Infrastructure.Repositories.AddressesRepositories;
using KBDTypeServer.Infrastructure.Repositories.CartItemRepositories;
using KBDTypeServer.Infrastructure.Repositories.WishListRepositories;
using KBDTypeServer.Application.Services.AuthServices;
using KBDTypeServer.Application.Services.UserServices;
using KBDTypeServer.Application.Services.AddressService;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Application.Mapping;


var builder = WebApplication.CreateBuilder(args);

// Додавання контексту бази даних
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Інші сервіси
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/// <summary>
/// <!-- Додавання сервісів -->-->
/// </summary>
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IAddressService, AddressService>();

/// <summary>
/// <!-- Додавання репозиторіїв -->-->
/// </summary>
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();

/// <summary>
/// <!-- Додавання AutoMapper -->-->
/// </summary>
builder.Services.AddAutoMapper(typeof(KBDTypeServer.Application.Mapping.MappingProfile));


/// <summary>
/// Налаштування Identity
/// </summary>
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 0;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

/// <summary>
/// Налаштування CORS
/// </summary>
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

/// <summary>
/// Налаштування кукі для автентифікації
/// </summary>
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsProduction())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}



app.Run();