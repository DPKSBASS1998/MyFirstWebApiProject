using Microsoft.AspNetCore.Mvc;
using KBDTypeServer.Application;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KBDTypeServer.Application.DTOs;
using KBDTypeServer.Application.DTOs.ProductDtos;
using KBDTypeServer.Application.Services.ProductServices;
using KBDTypeServer.Infrastructure;
using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Domain.Entities.ProductEntity;
using KBDTypeServer.Infrastructure.Repositories.ProductRepositories;

namespace KBDTypeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProductsAsync(cancellationToken);
            return Ok(products);
        }
    }
}
