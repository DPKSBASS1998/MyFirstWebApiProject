using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KBDTypeServer.Domain.Enums;

namespace KBDTypeServer.Domain.Entities.ProductEntity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        // Назва товару (спільна для будь-якого продукту)
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public ProductType ProductType { get; set; } // Тип продукту, наприклад, "Switch", "Keycap", "Keyboard" тощо

        // Опис (може бути пустим або null, якщо не обов’язковий)
        public string? Description { get; set; }

        // URL або шлях до зображення товару (спільний атрибут)
        public string? ImageUrl { get; set; }

        // Ціна (універсальна для всіх типів продуктів)
        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        // Кількість на складі — припустимо, що будь-який товар має свій склад
        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } = 0;

        // Виробник (теж може бути спільним атрибутом для різних товарів)
        [MaxLength(100)]
        public string Manufacturer { get; set; }

        // Поле, яке не мапиться в БД, але зручно для перевірки: чи є товар на складі
        [NotMapped]
        public bool InStock => StockQuantity > 0;
    }
}
