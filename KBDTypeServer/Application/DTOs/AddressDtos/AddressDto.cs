using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs.AddressDtos
{
    public class AddressDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }

        public string? Apartment { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string PostalCode { get; set; }

    }
}
