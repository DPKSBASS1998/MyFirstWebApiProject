// src/Models/Users/Address.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBDTypeServer.Models.Users
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Region { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(150)]
        public string Street { get; set; }

        [MaxLength(20)]
        public string Apartment { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }

        // Foreign Key
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
