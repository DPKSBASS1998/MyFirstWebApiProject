using KBDTypeServer.Application.DTOs;

namespace KBDTypeServer.Application.Interfaces
{
    public interface IAddressService
    {
        Task<(bool Success, string Message)> SaveAddressAsync(string userId, AddressDto dto);
        Task<List<AddressDto>> GetAddressesAsync(string userId);

    }
}
