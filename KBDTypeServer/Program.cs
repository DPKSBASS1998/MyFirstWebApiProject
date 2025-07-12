using KBDTypeServer.Application;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using KBDTypeServer.Application.Services.OrderServices;
using KBDTypeServer.Application.Services.PaymentService;
using KBDTypeServer.Application.Services.ProductServices;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Application.Services.ElitApiServices;


var builder = WebApplication.CreateBuilder(args);

// Configure NEW database connection

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("AWSConnection")));

// Add controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// <!-- Service Registration -->-->
/// </summary>
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IElitApiService, ElitApiService>();


/// <summary>
/// <!-- Repository Registration -->-->
/// </summary>
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();

/// <summary>
/// <!-- AutoMapper Configuration -->-->
/// </summary>
builder.Services.AddAutoMapper(typeof(KBDTypeServer.Application.Mapping.MappingProfile));


/// <summary>
/// Identity password Configuration
/// </summary>
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    // Password configuration
    if (builder.Environment.IsDevelopment())
    {
        // Development: weak password policy for easier testing
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 1;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 0;
    }
    else
    {
        // Production: strong password policy for security
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 2;
    }
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

/// <summary>
/// Cookie Policy Configuration
/// </summary>
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
});

var app = builder.Build();


// Enable Swagger UI only in development for API documentation/testing
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS for all requests
app.UseHttpsRedirection();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

// Serve static files and fallback to index.html in production (for SPA)
if (app.Environment.IsProduction())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}



app.Run();