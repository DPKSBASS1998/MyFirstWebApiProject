using KBDTypeServer.Infrastructure.Data;
using KBDTypeServer.Domain.Factories;
using Microsoft.EntityFrameworkCore;
using KBDTypeServer.Domain.Entities.OrderEntity;

namespace KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> AddAsync(Order order, CancellationToken cancellationToken)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }
        if (order.Id != 0)
        {
            throw new ArgumentException("Order ID must be zero for a new order", nameof(order));
        }
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }
        
    public async Task<bool> DeleteAsync(Order order, CancellationToken cancellationToken)
    {
        _context.Orders.Remove(order);
        if (order == null)
        {
            return false; // If the order is null, we cannot delete it
            throw new ArgumentNullException(nameof(order), "Order cannot be null");   
        }
        await _context.SaveChangesAsync(cancellationToken);
        return true;
        
    }

    public async Task<List<Order?>> GetAllByUserIdAsync(int userId,CancellationToken cancellationToken)
    {
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero");
        }
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Order ID must be greater than zero");
        }
        return await _context.Orders.FindAsync(new object[] { id }, cancellationToken).AsTask();
    }

    public async Task<Order?> UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }
        if (order.Id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(order.Id), "Order ID must be greater than zero for an update");
        }
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
        return order;

    }
}
