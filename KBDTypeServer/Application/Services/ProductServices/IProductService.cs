using System.Collections.Generic;
using KBDTypeServer.Application.DTOs.ProductDtos;
namespace KBDTypeServer.Application.Services.ProductServices
{
    public interface IProductService
    {
        
        Task<ProductDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
  
        Task<List<ProductDto?>> GetAllProductsAsync(CancellationToken cancellationToken);

        Task<List<ProductDto?>> GetWithFilterAsync(ProductFilterDto productFilterDto, CancellationToken cancellationToken);

    }
}
