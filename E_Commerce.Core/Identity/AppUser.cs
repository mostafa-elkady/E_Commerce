using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Core.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }

    }
}
