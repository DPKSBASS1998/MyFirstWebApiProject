using KBDTypeServer.Application.Interfaces;
using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using KBDTypeServer.Application.DTOs;


namespace KBDTypeServer.Infrastructure.Services.SwitchServices
{
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

        public async Task<List<Switches>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _db.Switches.ToListAsync(cancellationToken);
        }

        public async Task<Switches> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.Switches.FindAsync(id, cancellationToken);
        }

        public async Task<List<Switches>> FilterAsync(SwitchFilterDto f, CancellationToken cancellationToken)
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

    
}
