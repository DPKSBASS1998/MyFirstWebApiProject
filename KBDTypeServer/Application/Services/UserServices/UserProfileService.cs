using Microsoft.AspNetCore.Identity;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Application.DTOs.UserDtos;
using AutoMapper;

namespace KBDTypeServer.Application.Services.UserServices
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserProfileService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<UserProfileDto> GetUserProfileAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<UserProfileDto> UpdateUserAsync(UserProfileDto userProfileDto, int userId)
        {
            if (userProfileDto == null)
                throw new ArgumentNullException(nameof(userProfileDto));

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            if (user.Id != userId)
                throw new InvalidOperationException($"User ID mismatch: expected {userId}, but found {user.Id}.");

            // Оновлення властивостей через AutoMapper
            _mapper.Map(userProfileDto, user);
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"User update failed: {errors}");
            }

            return _mapper.Map<UserProfileDto>(user);
        }
    }
}
