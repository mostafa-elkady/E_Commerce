using E_Commerce.Api.Errors;
using E_Commerce.Api.Extensions;
using E_Commerce.repository.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            //  [ **Extensions * * ]
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityExtension(builder.Configuration);
            //Allow Dependency Injection For DbContext
            builder.Services.AddDbContext<StoreContext>(Options =>
            {

                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //Allow Dependency Injection For IdentityDataContext
            builder.Services.AddDbContext<IdentityDataContext>(Options =>
            {

                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });

            #endregion

            var app = builder.Build();
            //DataSeeding In database  [ **  Extension  ** ]
            await DatabaseInitializer.InitializeDbAsync(app);

            #region Configuration
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<CustomMiddlewareException>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
