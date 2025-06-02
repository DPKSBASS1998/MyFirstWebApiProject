namespace KBDTypeServer.Application.DTOs
{
    /// <summary>
    /// DTO для реєстрації користувача (передається з WebApi до Application)
    /// </summary>
    public class RegisterDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Якщо true, користувач отримає роль "Manager"
        /// </summary>
        public bool AssignAsManager { get; set; }
    }
}
