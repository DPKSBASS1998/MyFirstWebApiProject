using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KBDTypeServer.Models.Data;
using KBDTypeServer.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KBDTypeServer.Services
{
    /// <summary>
    /// Defines operations for querying and retrieving switch products.
    /// </summary>
    public interface ISwitchService
    {
        Task<List<Switches>> GetAllAsync();
        Task<Switches> GetByIdAsync(int id);

        /// <summary>
        /// Повертає всі перемикачі, що відповідають заданим фільтрам.
        /// </summary>
        Task<List<Switches>> FilterAsync(SwitchFilterDto filter);
    }

    /// <summary>
    /// Entity-framework–backed implementation of <see cref="ISwitchService"/>.
    /// </summary>
    public class SwitchService : ISwitchService
    {
        private readonly ApplicationDbContext _db;

        public SwitchService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Switches>> GetAllAsync()
        {
            return await _db.Switches.ToListAsync();
        }

        public async Task<Switches> GetByIdAsync(int id)
        {
            return await _db.Switches.FindAsync(id);
        }

        public async Task<List<Switches>> FilterAsync(SwitchFilterDto f)
        {
            IQueryable<Switches> q = _db.Switches;

            // Якщо всі фільтри за замовчуванням (null або порожні), повертаємо без обмежень
            if (string.IsNullOrWhiteSpace(f.Type)
              && string.IsNullOrWhiteSpace(f.Manufacturer)
              && !f.MinOperatingForce.HasValue && !f.MaxOperatingForce.HasValue
              && !f.MinTotalTravel.HasValue && !f.MaxTotalTravel.HasValue
              && !f.MinPreTravel.HasValue && !f.MaxPreTravel.HasValue
              && !f.MinTactilePosition.HasValue && !f.MaxTactilePosition.HasValue
              && !f.MinTactileForce.HasValue && !f.MaxTactileForce.HasValue
              && !f.PinCount.HasValue
              && !f.MinPrice.HasValue && !f.MaxPrice.HasValue
              && !f.InStock.HasValue)
            {
                return await _db.Switches.ToListAsync();
            }

            // Тип
            if (!string.IsNullOrWhiteSpace(f.Type))
                q = q.Where(s => s.Type == f.Type);

            // Виробник
            if (!string.IsNullOrWhiteSpace(f.Manufacturer))
                q = q.Where(s => s.Manufacturer == f.Manufacturer);

            // OperatingForce
            if (f.MinOperatingForce.HasValue)
                q = q.Where(s => s.OperatingForce >= f.MinOperatingForce.Value);
            if (f.MaxOperatingForce.HasValue)
                q = q.Where(s => s.OperatingForce <= f.MaxOperatingForce.Value);

            // TotalTravel
            if (f.MinTotalTravel.HasValue)
                q = q.Where(s => s.TotalTravel >= f.MinTotalTravel.Value);
            if (f.MaxTotalTravel.HasValue)
                q = q.Where(s => s.TotalTravel <= f.MaxTotalTravel.Value);

            // PreTravel
            if (f.MinPreTravel.HasValue)
                q = q.Where(s => s.PreTravel >= f.MinPreTravel.Value);
            if (f.MaxPreTravel.HasValue)
                q = q.Where(s => s.PreTravel <= f.MaxPreTravel.Value);

            // TactilePosition
            if (f.MinTactilePosition.HasValue)
                q = q.Where(s => s.TactilePosition >= f.MinTactilePosition.Value);
            if (f.MaxTactilePosition.HasValue)
                q = q.Where(s => s.TactilePosition <= f.MaxTactilePosition.Value);

            // TactileForce
            if (f.MinTactileForce.HasValue)
                q = q.Where(s => s.TactileForce >= f.MinTactileForce.Value);
            if (f.MaxTactileForce.HasValue)
                q = q.Where(s => s.TactileForce <= f.MaxTactileForce.Value);

            // PinCount (точне співпадіння: 3 або 5)
            if (f.PinCount.HasValue)
                q = q.Where(s => s.PinCount == f.PinCount.Value);

            // Price
            if (f.MinPrice.HasValue)
                q = q.Where(s => s.Price >= f.MinPrice.Value);
            if (f.MaxPrice.HasValue)
                q = q.Where(s => s.Price <= f.MaxPrice.Value);

            // Наявність на складі
            // Якщо InStock == true → фільтруємо ТІЛЬКИ ті, що є в наявності
            if (f.InStock.HasValue && f.InStock.Value == true)
                q = q.Where(s => s.StockQuantity > 0);


            return await q.ToListAsync();
        }
    }

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
        public int? MinTotalTravel { get; set; }

        /// <summary>Максимальна повна відстань ходу (моделі).</summary>
        public int? MaxTotalTravel { get; set; }

        /// <summary>Мінімальний попередній хід.</summary>
        public int? MinPreTravel { get; set; }

        /// <summary>Максимальний попередній хід.</summary>
        public int? MaxPreTravel { get; set; }

        /// <summary>Мінімальна позиція тактильного bump.</summary>
        public int? MinTactilePosition { get; set; }

        /// <summary>Максимальна позиція тактильного bump.</summary>
        public int? MaxTactilePosition { get; set; }

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
