using KBDTypeServer.Application.DTOs.OrderDtos;
using KBDTypeServer.Domain.Entities.OrderEntity;
using KBDTypeServer.Domain.Factories;
namespace KBDTypeServer.Application.Services.OrderServices
{
    public interface IOrderService
    {
        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        Task<OrderShowDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all orders for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>A list of orders associated with the user.</returns>
        Task<List<OrderShowDto?>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken);
        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <param name="userId">The ID of the user adding the order.</param>
        /// <returns>The added order.</returns>
        Task AddAsync(OrderCreateDto order, int userId, CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="order">The order to update.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>The updated order.</returns>
        Task<OrderShowDto?> UpdateAsync(OrderShowDto order, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes an existing order.
        /// </summary>
        /// <param name="order">The order to delete.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(OrderShowDto order, CancellationToken cancellationToken);
    }
}
