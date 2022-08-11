namespace GeolocationApi.Application.Exceptions
{
    public enum ExceptionCode
    {
        NotFound,
        BadRequest,
        InternalError,
    }

    public class ApplicationException : Exception
    {
        public ExceptionCode Code { get; set; }

        public ApplicationException(string message = null) : base(message)
        {
        }
    }


    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message = null) : base(message)
        {
            Code = ExceptionCode.NotFound;
        }
    }

    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message = null) : base(message)
        {
            Code = ExceptionCode.BadRequest;
        }
    }

    public class InternalErrorException : ApplicationException
    {
        public InternalErrorException(string message = null) : base(message)
        {
            Code = ExceptionCode.InternalError;
        }
    }
}
