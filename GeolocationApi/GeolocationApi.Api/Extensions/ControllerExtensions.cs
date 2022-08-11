using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace GeolocationApi.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult  Unwrap<TResult, TContract>( this Result<TResult> result, Func<TResult,TContract> mapper)
        {
            return result.Match(obj =>
            {
                var response = mapper(obj);
                return new JsonResult(response);
            }, 
            exception =>
            {
                if (exception is Application.Exceptions.ApplicationException applicationException)
                {
                    return applicationException.ToJsonResult();
                }

                return exception.ToJsonResult();
            });
        }
    }
}
