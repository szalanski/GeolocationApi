namespace GeolocationApi.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ExceptionCode Code { get; set; }

        public ApplicationException(string message = null) : base(message)
        {
        }
    }
}
