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
            throw new ArgumentNullException(nameof(order), "Order cannot be null");   
        }
        await _context.SaveChangesAsync(cancellationToken);
        return true;
        
    }

    public Task<Order?> GetByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(paymentIntentId))
        {
            throw new ArgumentException("Payment Intent ID cannot be null or empty", nameof(paymentIntentId));
        }
        return _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.PaymentId == paymentIntentId, cancellationToken)
            ?? throw new KeyNotFoundException("Order with the specified Payment Intent ID not found");
    }

    public async Task<List<Order?>> GetAllByUserIdAsync(int userId,CancellationToken cancellationToken)
    {
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero");
        }
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .ThenInclude(item => item.Product)
            .ToListAsync(cancellationToken)
            ?? throw new KeyNotFoundException("No orders found for the specified user ID");
    }

    // Приклад для OrderRepository.cs з використанням EF Core
    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.Items) // Включаємо список товарів у замовленні
            .ThenInclude(oi => oi.Product) // Для кожного товару включаємо сам продукт
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
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
