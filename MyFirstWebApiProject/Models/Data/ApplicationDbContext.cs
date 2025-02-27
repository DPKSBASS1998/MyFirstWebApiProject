using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApiProject  .Models.Users;
using MyFirstWebApiProject.Models.Products;  // Підключення моделей товарів

namespace MyFirstWebApiProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Додавання таблиць для продуктів
        public DbSet<Product> Products { get; set; }
        public DbSet<KeyboardSwitch> Switches { get; set; }
        public DbSet<Barebone> Barebones { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<Keycaps> Keycaps { get; set; }

        // Інші DbSet, якщо потрібно
    }
}
