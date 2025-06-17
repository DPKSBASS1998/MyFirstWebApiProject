// Application/DTOs/AddressDto.cs
using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs
{
    public class AddressDto
    {
        // Nullable Id на виадок якщо адресу треба створити, якщо треба відредагувати то Id не може бути null
        public int? Id { get; set; }

        [MaxLength(100)]
        public string Region { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(150)]
        public string Street { get; set; }

        [MaxLength(20)]
        public string? Apartment { get; set; }

        public string Building { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }
    }
}
