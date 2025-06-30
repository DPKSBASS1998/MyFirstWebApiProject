using KBDTypeServer.Application.DTOs.UserDtos;

namespace KBDTypeServer.Application.Services.UserServices
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task<UserProfileDto> UpdateUserAsync(UserProfileDto userProfileDto, int userId);
    } 
}


