using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Domain.Entities.OrderEntity;
using KBDTypeServer.Domain.Entities.ProductEntity;
using KBDTypeServer.Domain.Enums;
using KBDTypeServer.Domain.Entities.AddressEnity;
using KBDTypeServer.Domain.Entities.UserEntity;

namespace KBDTypeServer.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Продукти
        public DbSet<Product> Products { get; set; }
        // Перемикачі
        public DbSet<Switch> Switches { get; set; }

        // Адреси для користувачів
        public DbSet<Address> Addresses { get; set; }
        // Замовлення
        public DbSet<Order> Orders { get; set; }
        // Товари в замовленнях
        public DbSet<OrderItem> OrderItems { get; set; }
        // Товари в кошику
        public DbSet<CartItem> CartItems { get; set; }
        // Товари в списку бажаного
        public DbSet<WishListItem> WishListItems { get; set; }
        // Користувачі
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /// <summary>
            /// Вказуємо що Product є базовим класом для Switch
            /// І використуємо таблицю Table Per Type (TPT) для зберігання даних
            /// </summary>
            modelBuilder.Entity<Product>()
                .ToTable("Products"); // Вказуємо, що Product є базовим класом і зберігається в таблиці  
            modelBuilder.Entity<Switch>().ToTable("Switches"); // Вказуємо, що Switch є Product і зберігається в таблиці Switches

            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.PhoneNumber);// Вказуємо, що PhoneNumber є альтернативним ключем для User

            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.NormalizedEmail ); // Вказуємо, що Email є альтернативним ключем для User

            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses) // Вказуємо, що User може мати багато адрес
                .WithOne(a => a.User) // Кожна Address має одного User
                .HasForeignKey(a => a.UserId); // Вказуємо зовнішній ключ для Address

            modelBuilder.Entity<User>()
                .HasMany(u => u.CartItems) // Вказуємо, що User може мати багато товарів в кошику
                .WithOne(ci => ci.User) // Кожен CartItem має одного User
                .HasForeignKey(ci => ci.UserId); // Вказуємо зовнішній ключ для CartItem

            modelBuilder.Entity<User>()
                .HasMany(u => u.WishListItems) // Вказуємо, що User може мати багато товарів в списку бажаного
                .WithOne(wi => wi.User) // Кожен WishlistItem має одного User
                .HasForeignKey(wi => wi.UserId); // Вказуємо зовнішній ключ для WishlistItem

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders) // Вказуємо, що User може мати багато замовлень
                .WithOne(o => o.User) // Кожне Order має одного User
                .HasForeignKey(o => o.UserId); // Вказуємо зовнішній ключ для Order

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items) // Вказуємо, що Order може мати багато OrderItem
                .WithOne(oi => oi.Order) // Кожен OrderItem має одного Order
                .HasForeignKey(oi => oi.OrderId); // Вказуємо зовнішній ключ для OrderItem

        }
    }
}
