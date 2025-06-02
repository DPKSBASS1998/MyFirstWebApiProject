using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Application.DTOs;

namespace KBDTypeServer.Application.Interfaces
{
    /// <summary>
    /// Defines operations for querying and retrieving switch products.
    /// </summary>
    public interface ISwitchService
    {
        Task<List<Switches>> GetAllAsync(CancellationToken cancellationToken);
        Task<Switches> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Повертає всі перемикачі, що відповідають заданим фільтрам.
        /// </summary>
        Task<List<Switches>> FilterAsync(SwitchFilterDto filter, CancellationToken cancellationToken);
    }
}
