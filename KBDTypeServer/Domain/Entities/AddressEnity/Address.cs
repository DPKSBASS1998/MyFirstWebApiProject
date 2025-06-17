using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KBDTypeServer.Domain.Entities.UserEntity;

namespace KBDTypeServer.Domain.Entities.AddressEnity
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the user who owns this address.
        /// </summary>

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required, MaxLength(100)]
        public string Region { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        [Required, MaxLength(150)]
        public string Street { get; set; }

        [MaxLength(20)]
        public string? Apartment { get; set; }

        [Required, MaxLength(20)]
        public string Building { get; set; }

        [Required, MaxLength(20)]
        public string PostalCode { get; set; }

    }
}
