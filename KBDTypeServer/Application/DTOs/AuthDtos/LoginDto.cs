using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs.AuthDtos
{
    public class LoginDto
    {
        [Required]
        public string Identifier { get; set; } // User's email address or mobile number for login
        [Required]
        public string Password { get; set; } // User's password
        public bool RememberMe { get; set; } // Indicates whether the user wants to be remembered (stay signed in)
    }
}
