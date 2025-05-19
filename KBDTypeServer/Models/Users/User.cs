using Microsoft.AspNetCore.Identity;

namespace KBDTypeServer.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}
