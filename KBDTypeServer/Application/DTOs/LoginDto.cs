namespace KBDTypeServer.Application.DTOs
{
    public class LoginDto 
    {
        /// <summary>
        /// Email користувача
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль користувача
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Запам'ятати користувача
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
