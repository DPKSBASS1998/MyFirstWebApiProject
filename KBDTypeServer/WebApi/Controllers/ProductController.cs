using System.Threading.Tasks;
using KBDTypeServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KBDTypeServer.Application.DTOs;

namespace KBDTypeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISwitchService _switchService;

        public ProductController(ISwitchService switchService)
        {
            _switchService = switchService;
        }

        /// <summary>
        /// Повертає всі товари (без фільтрації).
        /// GET /api/product
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var list = await _switchService.GetAllAsync(cancellationToken);
            return Ok(list);
        }

        /// <summary>
        /// Повертає один товар за його ID.
        /// GET /api/product/{id}
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken )
        {
            var item = await _switchService.GetByIdAsync(id, cancellationToken);
            if (item == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });
            return Ok(item);
        }

        /// <summary>
        /// Фільтрує товари за заданими параметрами.
        /// POST /api/product/filter
        /// Тіло запиту: SwitchFilterDto
        /// </summary>
        [HttpPost("filter")]
        public async Task<IActionResult> FilterProducts([FromBody] SwitchFilterDto filter, CancellationToken cancellationToken)
        {
            // Якщо filter == null, FilterAsync поверне всі товари за нашою логікою
            var result = await _switchService.FilterAsync(filter ?? new SwitchFilterDto(), cancellationToken);
            return Ok(result);
        }

    }
}
