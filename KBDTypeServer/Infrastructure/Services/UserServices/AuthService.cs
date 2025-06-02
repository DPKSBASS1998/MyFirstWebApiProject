using KBDTypeServer.Application.DTOs;
using KBDTypeServer.Application.Interfaces;
using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KBDTypeServer.WebApi.ViewModels;

namespace KBDTypeServer.Infrastructure.Services.UserServices
{
    public class AuthService : DependencyClass, IAuthService
    {
        public AuthService(
            ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
            : base(context, userManager, signInManager, roleManager)
        {
        }
        public async Task<(bool Success, string Message, string Username)> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (false, "Користувача не знайдено", null);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
                return (false, "Невірний пароль", null);

            return (true, "Успішний вхід", user.UserName);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var createResult = await _userManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
                return (false, string.Join("; ", createResult.Errors.Select(e => e.Description)));

            var role = model.AssignAsManager ? "Manager" : "User";
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
                return (false, $"Не вдалося призначити роль '{role}'");

            if (role == "User")
                await _signInManager.SignInAsync(user, isPersistent: false);

            return (true, "Реєстрація успішна");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
