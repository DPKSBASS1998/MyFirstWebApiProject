using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApiProject.Models.Products
{
    public class Keycaps
    {
        [Key] // Первинний ключ
        [ForeignKey("Product")] // Зв'язок із таблицею Product
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Profile { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public string Sublegends { get; set; }
        [Required]
        public string ManufacturingProcess {  get; set; }
        [Required]
        public string LayoutStandard { get; set; }
        [Required]
        public string ImagePath { get; set; } = "imagehere.png";
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від’ємною")]
        public int StockQuantity { get; set; } = 0; // 0 = Немає в наявності
    }
}
