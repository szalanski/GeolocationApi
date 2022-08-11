using System.Net;

namespace GeolocationApi.Application.Exceptions
{
    public class AlreadyExistsException : ApplicationException
    {
        public AlreadyExistsException(string message = null) : base(message)
        {
            Code = ExceptionCode.AlreadyExists;
            HttpStatus = HttpStatusCode.Conflict;
        }
    }
}
