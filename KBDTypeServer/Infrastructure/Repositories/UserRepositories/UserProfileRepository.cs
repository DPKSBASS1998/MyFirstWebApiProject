using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace KBDTypeServer.Infrastructure.Repositories.UserRepositories

{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserProfileRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task AddAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
