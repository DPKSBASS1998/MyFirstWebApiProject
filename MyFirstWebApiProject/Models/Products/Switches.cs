using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBDTypeServer.Models.Products
{
    /// <summary>
    /// Представляє механічний перемикач клавіатури.
    /// </summary>
    public class Switches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [Range(0, 2000)]
        public int OperatingForce { get; set; }

        [Required]
        [Range(0, 100)]
        public int TotalTravel { get; set; }

        [Required]
        [Range(0, 100)]
        public int PreTravel { get; set; }

        [Range(0, 100)]
        public int TactilePosition { get; set; } = 0;

        public int TactileForce { get; set; } = 0;

        [Required]
        [MaxLength(200)]
        public string ImagePath { get; set; } = "imagehere.png";

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } = 0;

        [Required]
        [MaxLength(100)]
        public string Manufacturer { get; set; }

        [Required]
        [Range(3, 5)]
        public int PinCount { get; set; }

        /// <summary>
        /// Ціна в гривнях.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [NotMapped]
        public bool InStock => StockQuantity > 0;

    }
}
