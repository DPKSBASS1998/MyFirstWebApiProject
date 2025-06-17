using KBDTypeServer.Domain.Entities.AddressEnity;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace KBDTypeServer.Infrastructure.Repositories.AdressesRepositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public async Task<Address> AddAdressAsync(Address adress)
        {
            if (adress == null) throw new ArgumentNullException(nameof(adress));
            _context.Addresses.Add(adress);
            await _context.SaveChangesAsync();
            return adress;
        }
        public async Task<Address?> GetAdressByIdAsync(int adressId)
        {
            if (adressId <= 0) throw new ArgumentOutOfRangeException(nameof(adressId), "Address ID must be greater than zero.");
            return await _context.Addresses.FindAsync(adressId);
        }
        public async Task<List<Address>> GetAllAdressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }
        public async Task UpdateAdressAsync(Address adress)
        {
            if (adress == null) throw new ArgumentNullException(nameof(adress));
            _context.Addresses.Update(adress);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAdressAsync(int adressId)
        {
            if (adressId <= 0) throw new ArgumentOutOfRangeException(nameof(adressId), "Address ID must be greater than zero.");
            var adress = await GetAdressByIdAsync(adressId);
            if (adress == null) throw new KeyNotFoundException($"Address with ID {adressId} not found.");
            _context.Addresses.Remove(adress);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> AdressExistsAsync(int adressId)
        {
            if (adressId <= 0) throw new ArgumentOutOfRangeException(nameof(adressId), "Address ID must be greater than zero.");
            return await _context.Addresses.AnyAsync(a => a.Id == adressId);
        }
        public async Task<bool> AdressExistsByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            return await _context.Addresses.AnyAsync(a => a.UserId == userId);

        }
    }
}
