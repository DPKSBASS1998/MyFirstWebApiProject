using Microsoft.AspNetCore.Mvc;
using KBDTypeServer.Application;
using KBDTypeServer.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Infrastructure.Repositories.AddressesRepositories;
using KBDTypeServer.Application.DTOs.AddressDtos;
using KBDTypeServer.Application.Services.AddressService;
using Microsoft.AspNetCore.Authorization;

namespace KBDTypeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : BaseController
    {
        private readonly IAddressService _addressService;
        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var userId = GetСurrentUserId();
            if (userId == null)
                return Unauthorized("User ID is invalid or not found.");
            var addresses = await _addressService.GetAllAddressesForCurrentUserAsync(userId.Value, cancellationToken);
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int addressId, CancellationToken cancellationToken)
        {
            var userId = GetСurrentUserId();
            if (userId == null)
                return Unauthorized("User ID is invalid or not found.");
            if (addressId <= 0)
                return BadRequest("Invalid address ID.");
            var address = await _addressService.GetAddressByIdAsync(addressId, userId.Value, cancellationToken);
            if (address == null)
                return NotFound($"Address with ID {addressId} not found.");
            return Ok(address);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult>Post([FromBody] AddressDto addressDto, CancellationToken cancellationToken)
        {
            if (addressDto == null)
                return BadRequest("Address data cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = GetСurrentUserId();
            if (userId == null )
                return Unauthorized("User ID is invalid or not found.");
            addressDto.UserId = userId.Value;
            var newAddress = await _addressService.AddAddressAsync(userId.Value, addressDto, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = newAddress.Id }, newAddress);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put ([FromBody] AddressDto addressDto, CancellationToken cancellationToken)
        {
            if (addressDto == null || addressDto.Id <= 0)
                return BadRequest("Invalid address data.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = GetСurrentUserId();
            if (userId == null)
                return Unauthorized("User ID is invalid or not found.");
            addressDto.UserId = userId.Value;
            var updatedAddress = await _addressService.UpdateAddressAsync(userId.Value, addressDto, cancellationToken);
            return Ok(updatedAddress);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Invalid address ID.");
            var userId = GetСurrentUserId();
            if (userId == null)
                return Unauthorized("User ID is invalid or not found.");
            var result = await _addressService.DeleteAddressAsync(userId.Value, id, cancellationToken);
            if (!result)
                return NotFound($"Address with ID {id} not found or could not be deleted.");
            return NoContent();
        }
    }
}
