using KBDTypeServer.Domain.Entities.AddressEnity;
namespace KBDTypeServer.Infrastructure.Repositories.AdressesRepositories
{
    public interface IAddressRepository
    {
        Task<Address> AddAdressAsync(Address adress);
        Task<Address?> GetAdressByIdAsync(int adressId);
        Task<List<Address>> GetAllAdressesAsync();
        Task UpdateAdressAsync(Address adress);
        Task DeleteAdressAsync(int adressId);
        Task<bool> AdressExistsAsync(int adressId);
        Task<bool> AdressExistsByUserIdAsync(string userId);
    }
}
