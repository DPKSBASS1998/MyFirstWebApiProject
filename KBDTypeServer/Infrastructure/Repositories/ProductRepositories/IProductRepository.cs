using KBDTypeServer.Domain.Entities.ProductEntity;

namespace KBDTypeServer.Infrastructure.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Додає новий продукт.
        /// </summary>
        /// <param name="product">Продукт для додавання.</param>
        /// <returns>Доданий продукт.</returns>
        Task<Product?> AddProductAsync(Product product, CancellationToken cancellationToken);
        /// <summary>
        /// Отримує продукт за його ідентифікатором.
        /// </summary>
        /// <param name="productId">Ідентифікатор продукту.</param>
        /// <returns>Продукт або null, якщо не знайдено.</returns>
        Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
        /// <summary>
        /// Отримує всі продукти.
        /// </summary>
        /// <returns>Список всіх продуктів.</returns>
        Task<List<Product?>> GetAllProductsAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Оновлює існуючий продукт.
        /// </summary>
        /// <param name="product">Продукт з оновленими даними.</param>
        Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        /// <summary>
        /// Видаляє продукт за його ідентифікатором.
        /// </summary>
        /// <param name="productId">Ідентифікатор продукту для видалення.</param>
        Task<bool> DeleteProductAsync(int productId, CancellationToken cancellationToken);
        /// <summary>
        /// Перевіряє, чи існує продукт з вказаним ідентифікатором.
        /// </summary>
        /// <param name="productId">Ідентифікатор продукту.</param>
        /// <returns>True, якщо продукт існує; інакше - false.</returns>
        Task<bool> ProductExistsAsync(int productId, CancellationToken cancellationToken);


    }
}
