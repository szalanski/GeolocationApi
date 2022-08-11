using System.Net;

namespace GeolocationApi.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ExceptionCode Code { get; init; }
        public HttpStatusCode HttpStatus { get; init; }

        public ApplicationException(string message = null) : base(message)
        {
        }
    }
}
