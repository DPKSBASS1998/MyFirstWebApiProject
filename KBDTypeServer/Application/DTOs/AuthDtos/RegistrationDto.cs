using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs.AuthDtos
{
    public class RegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^380\d{9}$", ErrorMessage = "Phone number must start with 380 and contain 9 digits after.")]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
