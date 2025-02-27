using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebApiProject.Models.Products
{
    public class Barebone
    {
        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Layout { get; set; }

        [Required]
        public bool RGB { get; set; }

        [Required]
        public bool HotSwap { get; set; }

        [Required]
        public string Connection { get; set; }

        [Required]
        public string CaseMaterial { get; set; }

        [Required]
        public string Mount { get; set; }

        [Required]
        public string ImagePath { get; set; } = "imagehere.png";

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від’ємною")]
        public int StockQuantity { get; set; } = 0; // 0 = Немає в наявності
    }
}
