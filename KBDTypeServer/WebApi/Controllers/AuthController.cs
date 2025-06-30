// src/Controllers/AuthController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KBDTypeServer.Application.DTOs.AuthDtos;
using KBDTypeServer.WebApi;
using Microsoft.AspNetCore.Identity;
using KBDTypeServer.Application.Services.UserServices;
using KBDTypeServer.Application.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;


namespace KBDTypeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpGet("O")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var userId = GetСurrentUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");
            return Ok(userId.Value);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            if (registrationDto == null)
                return BadRequest("Registration data cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registrationResult = await _authService.RegisterAsync(registrationDto);
            if (!registrationResult)
                return BadRequest("Registration failed.");
            return Ok("User registered and logged in successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Login data cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.SignInAsync(loginDto);
            if (result)
            {
                return Ok("User logged in successfully.");
            }
            return Unauthorized("Invalid login attempt.");
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.SignOutAsync();
            if (result)
            {
                return Ok("User logged out successfully.");
            }
            return BadRequest("Logout failed.");
        }

    }
}
