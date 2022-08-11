using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GeolocationApi.Api.Extensions
{
    public static class ApplicationExceptionExtensions
    {
        public static IActionResult ToJsonResult(this Application.Exceptions.ApplicationException ex)
        {
            var jsonResult = new
            {
                status = ex.HttpStatus,
                error = ex.HttpStatus.ToString(),
                message = ex.Message
            };

            return new JsonResult(jsonResult)
            {
                StatusCode = (int)ex.HttpStatus
            };
        }

        public static IActionResult ToJsonResult(this Exception ex)
        {
            var jsonResult = new
            {
                status = HttpStatusCode.InternalServerError,
                error = HttpStatusCode.InternalServerError.ToString(),
                message = ex.Message
            };

            return new JsonResult(jsonResult)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
