using E_Commerce.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.repository.Data
{
    public static class IdentityDataContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    UserName = "MostafaAli",
                    Email = "MostafaAli@gmail.com",
                    DisplayName = "Mostafa Ali",
                    Address = new Address
                    {
                        FirstName = "Mostafa",
                        LastName = "Ali",
                        City = "Cairo",
                        Country = "Egypt",
                        Street = "Talaat Harb"

                    }
                };

                await userManager.CreateAsync(user, "P@ssw0rd");

            }
        }
    }
}
