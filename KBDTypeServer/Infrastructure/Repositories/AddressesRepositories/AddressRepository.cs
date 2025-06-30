using KBDTypeServer.Domain.Entities.AddressEnity;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace KBDTypeServer.Infrastructure.Repositories.AddressesRepositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public async Task<Address> AddAdressAsync(Address adress, CancellationToken cancellationToken)
        {
            if (adress == null)
                throw new ArgumentNullException(nameof(adress), "Address object cannot be null.");

            var newAddress = await _context.Addresses.AddAsync(adress, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newAddress.Entity;
        }

        public async Task<Address> GetAdressByIdAsync(int adressId, CancellationToken cancellationToken)
        {
            if (adressId <= 0)
                throw new ArgumentOutOfRangeException(nameof(adressId), "Address ID must be greater than zero.");

            return await _context.Addresses.FindAsync(adressId, cancellationToken);
        }

        public async Task<List<Address>> GetAllAdressesByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");

            return await _context.Addresses
                .Where(a => a.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Address> UpdateAdressAsync(Address adress, CancellationToken cancellationToken)
        {
            if (adress == null)
                throw new ArgumentNullException(nameof(adress), "Address object cannot be null.");

            var existing = await _context.Addresses.FindAsync(adress.Id, cancellationToken);
            if (existing == null)
                throw new KeyNotFoundException($"Address with ID {adress.Id} not found for update.");

            // Оновлюємо поля (щоб уникнути втрати даних)
            _context.Entry(existing).CurrentValues.SetValues(adress);

            await _context.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteAdressAsync(Address address, CancellationToken cancellationToken)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address object cannot be null.");

            var existing = await _context.Addresses.FindAsync(address.Id, cancellationToken);
            if (existing == null)
                throw new KeyNotFoundException($"Address with ID {address.Id} not found for deletion.");

            _context.Addresses.Remove(existing);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
