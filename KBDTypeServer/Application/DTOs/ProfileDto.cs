namespace KBDTypeServer.Application.DTOs
{
    /// <summary>
    /// ДТО для збереження/отримання профілю користувача
    /// </summary>
    public class ProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }
}
