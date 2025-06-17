using KBDTypeServer.Domain.Entities.OrderEntity;

namespace KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<List<Order>> GetAllAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
}
