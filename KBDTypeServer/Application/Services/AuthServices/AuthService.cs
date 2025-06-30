using AutoMapper;
using KBDTypeServer.Application.DTOs.AuthDtos;
using KBDTypeServer.Infrastructure.Repositories.UserRepositories;
using KBDTypeServer.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Identity;

namespace KBDTypeServer.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AuthService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<bool> RegisterAsync(RegistrationDto registrationDto)
        {
            if (registrationDto == null)
            {
                throw new ArgumentNullException(nameof(registrationDto), "Registration data cannot be null.");
            }
            // Map the DTO to the User entity
            var user = _mapper.Map<User>(registrationDto);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }
            // Automatically sign in the user after registration
            var signInResult = await _signInManager.PasswordSignInAsync(
                user.UserName,
                registrationDto.Password,
                true,
                false);
            if (!signInResult.Succeeded)
            {
                throw new Exception("Automatic sign-in after registration failed.");
            }
            user.LastLoginAt = DateTime.UtcNow;
            return result.Succeeded;
            
        }

        public async Task<bool> SignInAsync(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                    throw new ArgumentNullException(nameof(loginDto), "Login data cannot be null.");

                var user = await _userManager.FindByEmailAsync(loginDto.Identifier) ?? await _userManager.FindByNameAsync(loginDto.Identifier);
                if (user == null)
                    throw new InvalidOperationException("Користувача з таким ідентифікатором не знайдено.");

                var result = await _signInManager.PasswordSignInAsync(
                                                                    user.UserName, 
                                                                    loginDto.Password,
                                                                    loginDto.RememberMe,
                                                                    false);
                if (!result.Succeeded)
                    throw new InvalidOperationException("Невірний пароль.");
                user.LastLoginAt = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                // Тут можна залогувати помилку, якщо потрібно
                // _logger.LogError(ex, "Помилка при вході користувача");
                throw new InvalidOperationException($"Помилка входу: {ex.Message}", ex);
            }
        }

        public async Task<bool> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
