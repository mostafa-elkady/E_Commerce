using E_Commerce.Core.Identity;
using E_Commerce.repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Api.Extensions
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeDbAsync(WebApplication app)
        {
            // Update Database 
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //automatic Update Database after Migrations
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var context = services.GetRequiredService<StoreContext>();
                var Identitycontext = services.GetRequiredService<IdentityDataContext>();
                if ((await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await context.Database.MigrateAsync();
                }
                if ((await Identitycontext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await Identitycontext.Database.MigrateAsync();
                }
                //Apply Data Seeding
                await StoreContextSeed.SeedDataAsync(context);
                await IdentityDataContextSeed.SeedUsersAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "an error occurred during apply the migration");
            }
        }
    }
}
