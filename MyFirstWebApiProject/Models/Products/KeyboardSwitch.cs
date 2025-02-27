using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApiProject.Models.Products
{
    public class KeyboardSwitch
    {
        [Key] // Первинний ключ
        [ForeignKey("Product")] // Зв'язок із таблицею Product
        public int ProductId { get; set; }
        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Type { get; set; }

        [Required] 
        public int OperatingForce { get; set; }

        [Required] 
        public int TotalTravel { get; set; }

        [Required] 
        public int PreTravel { get; set; }

        public int TactilePosition { get; set; } = 0;
        public int TactileForce { get; set; } = 0;
        [Required]
        public string ImagePath { get; set; } = "imagehere.png";
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від’ємною")]
        public int StockQuantity { get; set; } = 0; // 0 = Немає в наявності

    }
}
