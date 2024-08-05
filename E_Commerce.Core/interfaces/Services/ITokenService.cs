using E_Commerce.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Core.interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
