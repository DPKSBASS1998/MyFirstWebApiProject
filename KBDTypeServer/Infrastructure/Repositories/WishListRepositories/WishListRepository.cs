using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace KBDTypeServer.Infrastructure.Repositories.WishListRepositories
{
    public class WishListRepository
    {
        private readonly ApplicationDbContext _context;
        public WishListRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public async Task<WishListItem> AddWishListItemAsync(WishListItem wishListItem)
        {
            if (wishListItem == null) throw new ArgumentNullException(nameof(wishListItem));
            _context.Set<WishListItem>().Add(wishListItem);
            await _context.SaveChangesAsync();
            return wishListItem;
        }
        public async Task<WishListItem?> GetWishListItemByIdAsync(int wishListItemId)
        {
            if (wishListItemId <= 0) throw new ArgumentOutOfRangeException(nameof(wishListItemId), "Wish List Item ID must be greater than zero.");
            return await _context.Set<WishListItem>().FindAsync(wishListItemId);
        }
        public async Task<List<WishListItem>> GetAllWishListItemsAsync()
        {
            return await _context.Set<WishListItem>().ToListAsync();
        }
        public async Task UpdateWishListItemAsync(WishListItem wishListItem)
        {
            if (wishListItem == null) throw new ArgumentNullException(nameof(wishListItem));
            _context.Set<WishListItem>().Update(wishListItem);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteWishListItemAsync(int wishListItemId)
        {
            if (wishListItemId <= 0) throw new ArgumentOutOfRangeException(nameof(wishListItemId), "Wish List Item ID must be greater than zero.");
            var wishListItem = await GetWishListItemByIdAsync(wishListItemId);
            if (wishListItem == null) throw new KeyNotFoundException($"Wish List Item with ID {wishListItemId} not found.");
            _context.Set<WishListItem>().Remove(wishListItem);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> WishListItemExistsAsync(int wishListItemId)
        {
            if (wishListItemId <= 0) throw new ArgumentOutOfRangeException(nameof(wishListItemId), "Wish List Item ID must be greater than zero.");
            return await _context.Set<WishListItem>().AnyAsync(wli => wli.Id == wishListItemId);
        }
        public async Task<List<WishListItem>> GetWishListItemsByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
            return await _context.Set<WishListItem>()
                .Where(wli => wli.UserId == userId)
                .ToListAsync();

        }
    }
}
