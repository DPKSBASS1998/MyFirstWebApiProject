using AutoMapper;
using KBDTypeServer.Application.DTOs.ProductDtos;
using KBDTypeServer.Infrastructure.Repositories.ProductRepositories;

namespace KBDTypeServer.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<ProductUniversalDto?>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAllProductsAsync(cancellationToken);
            if (result == null || result.Count == 0)
            {
                throw new KeyNotFoundException("No products found.");
            }
            return _mapper.Map<List<ProductUniversalDto?>>(result);
        }

        public Task<ProductDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto?>> GetWithFilterAsync(ProductFilterDto productFilterDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
