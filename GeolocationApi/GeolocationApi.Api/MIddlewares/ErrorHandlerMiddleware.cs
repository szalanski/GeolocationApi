using System.Net;

namespace GeolocationApi.Api.MIddlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _request;

        public ErrorHandlerMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                var jsonResult = new
                {
                    status = HttpStatusCode.InternalServerError,
                    error = HttpStatusCode.InternalServerError.ToString(),
                    message = ex.Message
                };

                context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(jsonResult);
            }
        }
    }
}
