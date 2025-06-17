using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KBDTypeServer.Domain.Enums;

namespace KBDTypeServer.Domain.Entities.ProductEntity
{
    /// <summary>
    /// Представляє механічний перемикач клавіатури.
    /// </summary>
    public class Switch : Product
    {
        // Тип перемикача (наприклад, Linear, Tactile, Clicky)
        [Required]
        [MaxLength(50)]
        public SwitchType SwitchType { get; set; }

        // Сила спрацьовування в грамах
        [Required]
        [Range(0, 2000)]
        public int OperatingForce { get; set; }

        // Загальний хід у мм
        [Required]
        [Range(0, 100)]
        public double TotalTravel { get; set; }
        
        // Хід до спрацювання
        [Required]
        [Range(0, 100)]
        public double PreTravel { get; set; }

        //Позиція тактильно спрацювання якщо є
        [Range(0, 100)]
        public double TactilePosition { get; set; } = 0;

        // Тактильна сила, якщо є тактильний «помітний» відскік
        public int TactileForce { get; set; } = 0;

        // PinCount (кількість контактів у перемикача, наприклад, 3 або 5)
        [Required]
        [Range(3, 5)]
        public int PinCount { get; set; }
    }
}
