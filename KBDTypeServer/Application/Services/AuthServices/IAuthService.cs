using KBDTypeServer.Application.DTOs.AuthDtos;
namespace KBDTypeServer.Application.Services.AuthServices
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user with the provided registration model.
        /// </summary>
        /// <param name="registrationDto">The model containing user registration details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> RegisterAsync(RegistrationDto registrationDto);
        /// <summary>
        /// Logs in a user with the provided login model.
        /// </summary>
        /// <param name="loginDto">The model containing user login details.</param>
        /// <returns>A task representing the asynchronous operation, returning a JWT token if successful.</returns>
        Task<bool> SignInAsync(LoginDto loginDto);
        /// <summary>
        /// Signs out the currently authenticated user.
        /// </summary>
        Task<bool> SignOutAsync();
        /// <summary>
        /// Checks if the user is authenticated.
        /// </summary
    }
}
