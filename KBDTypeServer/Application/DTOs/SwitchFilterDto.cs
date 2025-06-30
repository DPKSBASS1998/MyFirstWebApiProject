namespace KBDTypeServer.Application.DTOs
{
    /// <summary>
    /// DTO для передачі параметрів фільтрації.
    /// </summary>
    public class SwitchFilterDto
    {
        /// <summary>Тип перемикача (наприклад: "linear", "tactile", "clicky").</summary>
        public string? Type { get; set; }

        /// <summary>Виробник перемикача.</summary>
        public string? Manufacturer { get; set; }

        /// <summary>Мінімальна сила спрацювання (gf).</summary>
        public int? MinOperatingForce { get; set; }

        /// <summary>Максимальна сила спрацювання (gf).</summary>
        public int? MaxOperatingForce { get; set; }

        /// <summary>Мінімальна повна відстань ходу (моделі).</summary>
        public double? MinTotalTravel { get; set; }

        /// <summary>Максимальна повна відстань ходу (моделі).</summary>
        public double? MaxTotalTravel { get; set; }

        /// <summary>Мінімальний попередній хід.</summary>
        public double? MinPreTravel { get; set; }

        /// <summary>Максимальний попередній хід.</summary>
        public double? MaxPreTravel { get; set; }

        /// <summary>Мінімальна позиція тактильного bump.</summary>
        public double? MinTactilePosition { get; set; }

        /// <summary>Максимальна позиція тактильного bump.</summary>
        public double? MaxTactilePosition { get; set; }

        /// <summary>Мінімальна сила тактильного bump.</summary>
        public int? MinTactileForce { get; set; }

        /// <summary>Максимальна сила тактильного bump.</summary>
        public int? MaxTactileForce { get; set; }

        /// <summary>Кількість пінів (3 або 5).</summary>
        public int? PinCount { get; set; }

        /// <summary>Мінімальна ціна (грн).</summary>
        public decimal? MinPrice { get; set; }

        /// <summary>Максимальна ціна (грн).</summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Якщо true — повернути лише товари в наявності (StockQuantity > 0).
        /// Якщо false — усі товари.
        /// Якщо null — не фільтрувати за наявністю.
        /// </summary>
        public bool? InStock { get; set; }
    }
}
