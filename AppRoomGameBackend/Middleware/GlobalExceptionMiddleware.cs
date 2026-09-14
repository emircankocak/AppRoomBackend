using System.Text.Json;

namespace AppRoomGameBackend.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new
            {
                statusCode = 500,
                error = "Internal Server Error",
                message = "Sunucu tarafında beklenmeyen bir hata oluştu."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}

