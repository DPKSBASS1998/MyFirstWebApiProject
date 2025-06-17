using KBDTypeServer.Domain.Entities.OrderEntity;
using KBDTypeServer.Infrastructure.Data;
using KBDTypeServer.Domain.Factories;
using Microsoft.EntityFrameworkCore;

namespace KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(int id)
        => await _context.Orders.FindAsync(id);

    public async Task<List<Order>> GetAllAsync()
        => await _context.Orders.ToListAsync();

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}
