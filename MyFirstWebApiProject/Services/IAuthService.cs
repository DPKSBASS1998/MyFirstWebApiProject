// src/Services/AuthService.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApiProject.Data;
using MyFirstWebApiProject.Models.Users;
using MyFirstWebApiProject.Services.DTOs;

namespace MyFirstWebApiProject.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, string Username)> LoginAsync(LoginViewModel model);
        Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model);
        Task LogoutAsync();
        Task<ProfileDto> GetProfileAsync(string userId);
        Task<(bool Success, string Message)> UpdateProfileAsync(string userId, ProfileDto dto);
        Task<(bool Success, string Message)> SaveAddressAsync(string userId, AddressDto dto);
        /// <summary>
        /// Повертає список всіх адрес користувача
        /// </summary>
        Task<List<AddressDto>> GetAddressesAsync(string userId);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<(bool Success, string Message, string Username)> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (false, "Користувача не знайдено", null);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
                return (false, "Невірний пароль", null);

            return (true, "Успішний вхід", user.UserName);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model)
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

        public async Task<ProfileDto> GetProfileAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null;

            return new ProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address == null
                    ? new AddressDto()
                    : new AddressDto
                    {
                        Region = user.Address.Region,
                        City = user.Address.City,
                        Street = user.Address.Street,
                        Apartment = user.Address.Apartment,
                        PostalCode = user.Address.PostalCode
                    }
            };
        }

        public async Task<(bool Success, string Message)> UpdateProfileAsync(string userId, ProfileDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return (false, "Користувача не знайдено");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return (false, string.Join("; ", result.Errors.Select(e => e.Description)));

            return (true, "Профіль оновлено");
        }

        public async Task<(bool Success, string Message)> SaveAddressAsync(string userId, AddressDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return (false, "Користувача не знайдено");

            if (user.Address == null)
            {
                user.Address = new Address { UserId = userId };
                _context.Addresses.Add(user.Address);
            }

            user.Address.Region = dto.Region;
            user.Address.City = dto.City;
            user.Address.Street = dto.Street;
            user.Address.Apartment = dto.Apartment;
            user.Address.PostalCode = dto.PostalCode;

            await _context.SaveChangesAsync();
            return (true, "Адресу збережено");
        }
        public async Task<List<AddressDto>> GetAddressesAsync(string userId)
        {
            return await _context.Addresses
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .Select(a => new AddressDto
                {
                    Region = a.Region,
                    City = a.City,
                    Street = a.Street,
                    Apartment = a.Apartment,
                    PostalCode = a.PostalCode
                })
                .ToListAsync();
        }
    }
}
