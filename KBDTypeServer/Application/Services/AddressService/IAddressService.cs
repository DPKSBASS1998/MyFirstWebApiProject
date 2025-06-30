using KBDTypeServer.Application.DTOs.AddressDtos;
namespace KBDTypeServer.Application.Services.AddressService

{
    public interface IAddressService
    {
        Task<AddressDto> GetAddressByIdAsync(int userId, int addressId, CancellationToken cancellationToken);
        Task<AddressDto> UpdateAddressAsync(int userId, AddressDto addressDto, CancellationToken cancellationToken);
        Task<AddressDto> AddAddressAsync(int userId, AddressDto addressDto, CancellationToken cancellationToken);
        Task<bool> DeleteAddressAsync(int userId, int addressId, CancellationToken cancellationToken);
        Task<List<AddressDto>> GetAllAddressesForCurrentUserAsync(int userId, CancellationToken cancellationToken);

    }
}
