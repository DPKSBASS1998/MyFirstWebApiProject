using KBDTypeServer.Application.DTOs;

namespace KBDTypeServer.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<ProfileDto> GetProfileAsync(string userId);
        Task<(bool Success, string Message)> UpdateProfileAsync(string userId, ProfileDto dto);

    }
}
