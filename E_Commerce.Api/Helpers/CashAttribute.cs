using E_Commerce.Core.interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.Api.Helpers
{
    public class CashAttribute : Attribute, IAsyncActionFilter
    {

        private readonly int _time;

        public CashAttribute(int time)
        {
            _time = time;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashKey = GenerateKeyFromResponse(context.HttpContext.Request);
            var _cashService = context.HttpContext.RequestServices.GetRequiredService<ICashRepository>();
            var cashResponse = await _cashService.GetCashResponseAsync(cashKey);
            if (cashResponse is not null)
            {
                var result = new ContentResult
                {
                    Content = cashResponse,
                    ContentType = "Application/json",
                    StatusCode = 200
                };
                context.Result = result;
                return;
            }

            var executedContext = await next();
            if (executedContext.Result is OkObjectResult response)
            {
                await _cashService.SetCashResponseAsync(cashKey, response.Value, TimeSpan.FromSeconds(_time));
            }
        }

        private string GenerateKeyFromResponse(HttpRequest request)
        {
            StringBuilder key = new();
            key.Append($"{request.Path}");
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"{item}");
            }
            return key.ToString();
        }
    }
}
