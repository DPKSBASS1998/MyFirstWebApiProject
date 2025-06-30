using KBDTypeServer.Domain.Entities.OrderEntity;


namespace KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Order?>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<Order?> AddAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> UpdateAsync(Order order, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Order order, CancellationToken cancellationToken);
}
