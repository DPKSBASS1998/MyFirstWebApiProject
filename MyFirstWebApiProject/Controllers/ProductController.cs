using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApiProject.Services;  // де оголошені ISwitchService і SwitchFilterDto

namespace MyFirstWebApiProject.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            var list = await _switchService.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// Повертає один товар за його ID.
        /// GET /api/product/{id}
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _switchService.GetByIdAsync(id);
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
        public async Task<IActionResult> FilterProducts([FromBody] SwitchFilterDto filter)
        {
            // Якщо filter == null, FilterAsync поверне всі товари за нашою логікою
            var result = await _switchService.FilterAsync(filter ?? new SwitchFilterDto());
            return Ok(result);
        }

    }
}
