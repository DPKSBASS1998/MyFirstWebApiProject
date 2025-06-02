using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.WebApi.ViewModels
{
    /// <summary>
    /// Модель для реєстрації користувача
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Якщо true, призначити роль "Manager"
        /// </summary>
        public bool AssignAsManager { get; set; }
    }
}
