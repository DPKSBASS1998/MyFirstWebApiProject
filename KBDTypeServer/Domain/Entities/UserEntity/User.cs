using System.ComponentModel.DataAnnotations;
using KBDTypeServer.Domain.Entities.AddressEnity;
using KBDTypeServer.Domain.Entities.OrderEntity;
using Microsoft.AspNetCore.Identity;

namespace KBDTypeServer.Domain.Entities.UserEntity
{
    public class User : IdentityUser<int>
    {
        
        [Required]
        public string FirstName { get; set; } // Default to empty string to avoid null reference issues

        [Required]
        public string LastName { get; set; } // Default to empty string to avoid null reference issues

        [Required]
        [Phone]
        [MaxLength(13)]
        [RegularExpression(@"^380\d{9}$", ErrorMessage = "Phone number must start with 380 and contain 9 digits after.")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public override string? Email { get; set; } // Override to ensure email is required and valid

        public string? Gender { get; set; } // mekanic

        public DateTime? DateOfBirth { get; set; } // Nullable to allow for users who do not provide this information

        [Required]
        public DateTime CreatedAt { get; set; } // Timestamp for when the user was created

        [Required]
        public DateTime UpdatedAt { get; set; } // Timestamp for when the user was last updated
        public DateTime? LastLoginAt { get; set; } // Timestamp for the last login, nullable if never logged in

        /// <summary>
        /// Child tables
        /// </summary>
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<WishListItem>? WishListItems { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
