using System.Net;

namespace E_Commerce.Api.Errors
{
    public class CustomMiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddlewareException> _logger;
        private readonly IHostEnvironment _environment;

        public CustomMiddlewareException(RequestDelegate next, ILogger<CustomMiddlewareException> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception e)
            {

                _logger.LogError(e.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _environment.IsDevelopment() ? new ApiExceptionError((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace) : new ApiExceptionError((int)HttpStatusCode.InternalServerError);
                await httpContext.Response.WriteAsJsonAsync(response);
            }

        }
    }
}
