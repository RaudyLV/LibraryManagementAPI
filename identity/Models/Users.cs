using Microsoft.AspNetCore.Identity;

namespace identity.Models
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
