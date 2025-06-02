using KBDTypeServer.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KBDTypeServer.Application.Interfaces;
using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Infrastructure.Data;

namespace KBDTypeServer.Infrastructure.Services.UserServices
{
    public class UserProfileService : DependencyClass, IUserProfileService
    {

        public UserProfileService(
        ApplicationDbContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager)
        : base(context, userManager, signInManager, roleManager)
        {
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
    }
}
