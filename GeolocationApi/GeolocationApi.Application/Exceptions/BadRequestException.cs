using System.Net;

namespace GeolocationApi.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message = null) : base(message)
        {
            Code = ExceptionCode.BadRequest;
            HttpStatus = HttpStatusCode.BadRequest;
        }
    }
}
