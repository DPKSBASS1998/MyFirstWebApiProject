using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Infrastructure.Data;
namespace KBDTypeServer.Infrastructure.Repositories.WishListRepositories
{
    public interface IWishListRepository
    {
        /// <summary>
        /// Adds a new item to the wish list.
        /// </summary>
        /// <param name="wishListItem">The wish list item to add.</param>
        /// <returns>The added wish list item.</returns>
        Task<WishListItem> AddWishListItemAsync(WishListItem wishListItem);
        /// <summary>
        /// Gets a wish list item by its ID.
        /// </summary>
        /// <param name="wishListItemId">The ID of the wish list item.</param>
        /// <returns>The wish list item, or null if not found.</returns>
        Task<WishListItem?> GetWishListItemByIdAsync(int wishListItemId);
        /// <summary>
        /// Gets all items in the wish list.
        /// </summary>
        /// <returns>A list of all wish list items.</returns>
        Task<List<WishListItem>> GetAllWishListItemsAsync();
        /// <summary>
        /// Updates an existing wish list item.
        /// </summary>
        /// <param name="wishListItem">The wish list item with updated data.</param>
        Task UpdateWishListItemAsync(WishListItem wishListItem);
        /// <summary>
        /// Deletes a wish list item by its ID.
        /// </summary>
        /// <param name="wishListItemId">The ID of the wish list item to delete.</param>
        Task DeleteWishListItemAsync(int wishListItemId);
        /// <summary>
        /// Checks if a wish list item exists by its ID.
        /// </summary>
        /// <param name="wishListItemId">The ID of the wish list item.</param>
        /// <returns>True if the item exists; otherwise, false.</returns>
        Task<bool> WishListItemExistsAsync(int wishListItemId);

        /// <summary>
        /// Cheks if a wish list item exists for a specific user and product.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>"
        /// <returns> List of items for userID.</returns>
        Task<List<WishListItem>> GetWishListItemsByUserIdAsync(string userId);
    }
}
