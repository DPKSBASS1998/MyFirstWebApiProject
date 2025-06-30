using KBDTypeServer.Application.DTOs.AddressDtos;
using KBDTypeServer.Infrastructure.Repositories.AddressesRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Domain.Entities.AddressEnity;

namespace KBDTypeServer.Application.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public AddressService(IAddressRepository addressRepository, IMapper mapper, UserManager<User> userManager)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AddressDto> AddAddressAsync(int userId, AddressDto addressDto, CancellationToken cancellationToken)
        {
            addressDto.UserId = userId;
            var addressEntity = _mapper.Map<Address>(addressDto);
            var newAddress = await _addressRepository.AddAdressAsync(addressEntity, cancellationToken);
            return _mapper.Map<AddressDto>(newAddress);
        }

        public async Task<bool> DeleteAddressAsync(int userId, int addressId, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAdressByIdAsync(addressId, cancellationToken);
            if (address == null)
            {
                throw new KeyNotFoundException($"Address with ID {addressId} not found.");
            }
            if (address.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this address.");
            }
            return await _addressRepository.DeleteAdressAsync(address, cancellationToken);
        }

        public async Task<AddressDto> GetAddressByIdAsync(int userId,int addressId, CancellationToken cancellationToken)
        {
            if (addressId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(addressId), "Address ID must be greater than zero.");
            }
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            if (user.Id != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to access this address.");
            }
            var address = await _addressRepository.GetAdressByIdAsync(addressId, cancellationToken);
            if (address == null)
            {
                throw new KeyNotFoundException($"Address with ID {addressId} not found.");
            }
            return _mapper.Map<AddressDto>(address);
        }

        public async Task<List<AddressDto>> GetAllAddressesForCurrentUserAsync(int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
            var addresses = await _addressRepository.GetAllAdressesByUserIdAsync(userId, cancellationToken);
            if (addresses == null || !addresses.Any())
            {
                throw new KeyNotFoundException($"No addresses found for user with ID {userId}.");
            }
            return _mapper.Map<List<AddressDto>>(addresses);
        }

        public async Task<AddressDto> UpdateAddressAsync(int userId, AddressDto addressDto, CancellationToken cancellationToken)
        {
            if(userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto), "Address DTO cannot be null.");
            }
            if (addressDto.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this address.");
            }
            var updatedAddress = await _addressRepository.UpdateAdressAsync((_mapper.Map<Address>(addressDto)), cancellationToken);
            if (updatedAddress == null)
            {
                throw new KeyNotFoundException($"Address with ID {addressDto.Id} not found for update.");
            }
            return _mapper.Map<AddressDto>(updatedAddress);
        }
    }
}
