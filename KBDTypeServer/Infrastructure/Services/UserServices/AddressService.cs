using KBDTypeServer.Application.DTOs;
using KBDTypeServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using KBDTypeServer.Application.Interfaces;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace KBDTypeServer.Infrastructure.Services.UserServices
{
    public class AddressService : DependencyClass, IAddressService
    {
        public AddressService(
            ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
            : base(context, userManager, signInManager, roleManager)
        {
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
