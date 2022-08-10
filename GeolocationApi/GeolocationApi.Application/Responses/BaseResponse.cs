namespace GeolocationApi.Application.Responses
{
    public abstract record BaseResponse<T> where T : class
    {
        public T Data { get; set; }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }

        public BaseResponse(bool succeeded = true, string message = "",T data = null, List<string> validationErrors = null)
        {
            Succeeded = succeeded;
            Message = message;
            ValidationErrors = validationErrors;
            Data = data;
        }
    }
    
}
