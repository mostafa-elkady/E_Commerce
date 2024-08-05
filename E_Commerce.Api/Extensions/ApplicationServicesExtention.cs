using E_Commerce.Api.Errors;
using E_Commerce.Api.Helpers;
using E_Commerce.Core;
using E_Commerce.Core.interfaces.Repositories;
using E_Commerce.Core.interfaces.Services;
using E_Commerce.repository;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Stripe;

namespace E_Commerce.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            // Add services to the container.

            Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                // Use descriptive enum names
                c.UseInlineDefinitionsForEnums();

            });

            // Allow Dependence Injection of All Controllers in program file
            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Allow Dependence Injection of UnitOfWork
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            // Allow Dependence Injection of Cart Controller
            Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));  
            // Allow Dependence Injection of Cash
            Services.AddScoped(typeof(ICashRepository), typeof(CashRepository));
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            Services.AddAutoMapper(typeof(MappingProfile));

            Services.Configure<ApiBehaviorOptions>(options =>
             options.InvalidModelStateResponseFactory = context =>
             {
                 var errors = context.ModelState
                 .Where(m => m.Value.Errors.Any())
                 .SelectMany(m => m.Value.Errors)
                 .Select(e => e.ErrorMessage).ToList();
                 return new BadRequestObjectResult(new ApiValidationError() { Errors = errors });
             });

            return Services;
        }
    }
}
