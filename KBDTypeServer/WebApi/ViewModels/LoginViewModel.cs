using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Domain.Entities
{
    /// <summary>
    /// Модель для логіну
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
