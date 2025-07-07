using System.Threading.Tasks;
using KBDTypeServer.Domain.Entities.ProductEntity;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace KBDTypeServer.Infrastructure.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<Product?> AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(product);
            _context.Set<Product>().Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
        public async Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be greater than zero.");
            return await _context.Set<Product>().FindAsync(productId, cancellationToken);
        }
        public async Task<List<Product?>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Set<Product>().ToListAsync(cancellationToken);
            if (result == null || result.Count == 0)
            {
                throw new KeyNotFoundException("No products found.");
            }
            return result;
        }
        public async Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            var updatedProduct = _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return updatedProduct.Entity;
        }
        public async Task<bool> DeleteProductAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be greater than zero.");
            var product = await GetProductByIdAsync(productId, cancellationToken);
            if (product == null) throw new KeyNotFoundException($"Product with ID {productId} not found.");
            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> ProductExistsAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be greater than zero.");
            return await _context.Set<Product>().AnyAsync(p => p.Id == productId, cancellationToken);
        }
        
    }
}
