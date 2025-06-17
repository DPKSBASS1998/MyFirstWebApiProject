using KBDTypeServer.Domain.Entities.UserEntity;
namespace KBDTypeServer.Infrastructure.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(string userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);
        Task<bool> UserExistsAsync(string userId);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber);



    }
}
