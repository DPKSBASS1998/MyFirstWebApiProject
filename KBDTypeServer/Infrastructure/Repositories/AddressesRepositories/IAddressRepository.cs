using KBDTypeServer.Domain.Entities.AddressEnity;

namespace KBDTypeServer.Infrastructure.Repositories.AddressesRepositories
{
    public interface IAddressRepository
    {
        Task<Address> AddAdressAsync(Address adress, CancellationToken cancellationToken);
        Task<Address> GetAdressByIdAsync(int adressId, CancellationToken cancellationToken);
        Task<List<Address>> GetAllAdressesByUserIdAsync(int userId, CancellationToken cancellationToken);
        Task<Address> UpdateAdressAsync(Address adress, CancellationToken cancellationToken);
        Task<bool> DeleteAdressAsync(Address address, CancellationToken cancellationToken);
    }
}
