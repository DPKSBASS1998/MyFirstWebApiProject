using KBDTypeServer.Application.DTOs;
using KBDTypeServer.WebApi.ViewModels;

namespace KBDTypeServer.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, string Username)> LoginAsync(LoginDto model);
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto model);
        Task LogoutAsync();
    }

    
}
