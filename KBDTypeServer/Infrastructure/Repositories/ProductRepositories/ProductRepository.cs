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

        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            _context.Set<Product>().Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be greater than zero.");
            return await _context.Set<Product>().FindAsync(productId);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Set<Product>().ToListAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int productId)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be greater than zero.");
            var product = await GetProductByIdAsync(productId);
            if (product == null) throw new KeyNotFoundException($"Product with ID {productId} not found.");
            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ProductExistsAsync(int productId)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be greater than zero.");
            return await _context.Set<Product>().AnyAsync(p => p.Id == productId);
        }
        public async Task<List<Product>> FilterProductsAsync(Func<IQueryable<Product>, IQueryable<Product>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            var query = _context.Set<Product>().AsQueryable();
            return await filter(query).ToListAsync();
        }
    }
}
