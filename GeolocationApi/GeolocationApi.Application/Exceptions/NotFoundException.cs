using System.Net;

namespace GeolocationApi.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message = null) : base(message)
        {
            Code = ExceptionCode.NotFound;
            HttpStatus = HttpStatusCode.NotFound;
        }
    }
}
