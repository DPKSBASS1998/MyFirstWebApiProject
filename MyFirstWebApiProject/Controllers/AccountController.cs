// src/Controllers/AuthController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApiProject.Services;
using MyFirstWebApiProject.Services.DTOs;
using System.Security.Claims;

namespace MyFirstWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new
                {
                    isAuthenticated = true,
                    username = User.Identity.Name
                });
            }
            return Ok(new { isAuthenticated = false });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Невірні дані для входу" });

            var (success, message, username) = await _authService.LoginAsync(model);
            if (!success)
                return Unauthorized(new { message });

            return Ok(new { message, username });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Некоректні дані для реєстрації" });

            var (success, message) = await _authService.RegisterAsync(model);
            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { message = "Успішно вийшли з системи" });
        }

        // Нові кінцеві точки для профілю

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var profile = await _authService.GetProfileAsync(userId);
            if (profile == null)
                return NotFound(new { message = "Профіль не знайдено" });

            return Ok(profile);
        }

        /// <summary>
        /// Оновлює і профіль, і адресу одним запитом
        /// </summary>
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            // 1) Оновлюємо основний профіль
            var (success, message) = await _authService.UpdateProfileAsync(userId, dto);
            if (!success)
                return BadRequest(new { message });

            // 2) Підтримуємо, якщо в DTO є Address — відразу зберігаємо його
            if (dto.Address != null)
            {
                var (addrSuccess, addrMsg) =
                    await _authService.SaveAddressAsync(userId, dto.Address);
                if (!addrSuccess)
                    return BadRequest(new { message = addrMsg });
            }

            return Ok(new { message });
        }
        [HttpGet("addresses")]
        public async Task<IActionResult> GetAddresses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();
            var list = await _authService.GetAddressesAsync(userId);
            return Ok(list);
        }

    }
}
