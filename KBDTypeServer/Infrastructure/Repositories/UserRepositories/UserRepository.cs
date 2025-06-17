using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace KBDTypeServer.Infrastructure.Repositories.UserRepositories

{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<User> AddUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.CreatedAt = DateTime.UtcNow; // Set the creation timestamp to the current time
            user.UpdatedAt = DateTime.UtcNow; // Set the update timestamp to the current time
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber));
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.UpdatedAt = DateTime.UtcNow; // Update the timestamp to the current time
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            var user = await GetUserByIdAsync(userId);
            if (user == null) throw new KeyNotFoundException($"User with ID {userId} not found.");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UserExistsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber));
            return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
        }
    }
}
