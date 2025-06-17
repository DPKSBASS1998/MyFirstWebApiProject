using KBDTypeServer.Domain.Entities;
namespace KBDTypeServer.Infrastructure.Repositories.CartItemRepository
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);
        Task<List<CartItem>> GetAllCartItemsAsync();
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);
        Task<bool> CartItemExistsAsync(int cartItemId);
        Task <List<CartItem>> GetCartItemsByUserIdAsync(string userId);
    }
}
