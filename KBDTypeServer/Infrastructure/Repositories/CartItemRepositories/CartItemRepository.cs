using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace KBDTypeServer.Infrastructure.Repositories.CartItemRepositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            if (cartItem == null) throw new ArgumentNullException(nameof(cartItem));
            _context.Set<CartItem>().Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
        public async Task<CartItem?> GetCartItemByIdAsync(int cartItemId)
        {
            if (cartItemId <= 0) throw new ArgumentOutOfRangeException(nameof(cartItemId), "Cart Item ID must be greater than zero.");
            return await _context.Set<CartItem>().FindAsync(cartItemId);
        }
        public async Task<List<CartItem>> GetAllCartItemsAsync()
        {
            return await _context.Set<CartItem>().ToListAsync();
        }
        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            if (cartItem == null) throw new ArgumentNullException(nameof(cartItem));
            _context.Set<CartItem>().Update(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCartItemAsync(int cartItemId)
        {
            if (cartItemId <= 0) throw new ArgumentOutOfRangeException(nameof(cartItemId), "Cart Item ID must be greater than zero.");
            var cartItem = await GetCartItemByIdAsync(cartItemId);
            if (cartItem == null) throw new KeyNotFoundException($"Cart Item with ID {cartItemId} not found.");
            _context.Set<CartItem>().Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CartItemExistsAsync(int cartItemId)
        {
            if (cartItemId <= 0) throw new ArgumentOutOfRangeException(nameof(cartItemId), "Cart Item ID must be greater than zero.");
            return await _context.Set<CartItem>().AnyAsync(ci => ci.Id == cartItemId);
        }

        public async Task<List<CartItem>> GetCartItemsByUserIdAsync(int userId)
        {
            if (string.IsNullOrEmpty(userId.ToString())) throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
            return await _context.Set<CartItem>()
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }
    }
}
