using KBDTypeServer.Domain.Entities.UserEntity;

namespace KBDTypeServer.Infrastructure.Repositories.UserRepositories
{
    public interface IUserProfileRepository
    {
        Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);      // якщо потрібне створення
        Task UpdateAsync(User user, CancellationToken cancellationToken);
        Task DeleteAsync(int userId, CancellationToken cancellationToken);
    }
}
