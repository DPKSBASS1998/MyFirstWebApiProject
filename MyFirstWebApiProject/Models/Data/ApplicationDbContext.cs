using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KBDTypeServer.Models.Products;
using KBDTypeServer.Models.Users;

namespace KBDTypeServer.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Продукти
        public DbSet<Switches> Switches { get; set; }

        // Адреси для користувачів
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Налаштувати one-to-one між User і Address
            builder.Entity<User>()
                   .HasOne(u => u.Address)
                   .WithOne(a => a.User)
                   .HasForeignKey<Address>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Якщо потрібні додаткові конфігурації моделей продуктів,
            // їх також можна прописати тут.
        }
    }
}
