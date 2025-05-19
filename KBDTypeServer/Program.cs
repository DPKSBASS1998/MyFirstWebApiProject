using System.IO;
using KBDTypeServer.Models.Data;
using KBDTypeServer.Models.Users;
using KBDTypeServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Додавання контексту бази даних
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додавання Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Інші сервіси
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISwitchService, SwitchService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Налаштування CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Роздача статичних файлів з wwwroot
app.UseStaticFiles();

// Підключення зовнішньої папки "assets" як статичної
// Використовуємо абсолютний шлях до папки, де фізично зберігаються зображення
var assetFolderPath = @"C:\Users\bvivl\Documents\ПРоектування інтерфейсу\my-app\src\assets";
if (Directory.Exists(assetFolderPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(assetFolderPath),
        RequestPath = "/assets"
    });
}
else
{
    // Приклад додаткового логування або обробки помилки
    Console.WriteLine($"Warning: Assets folder not found at {assetFolderPath}");
}

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

app.Run();