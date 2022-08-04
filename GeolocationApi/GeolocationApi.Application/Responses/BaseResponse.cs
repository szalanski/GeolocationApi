namespace GeolocationApi.Application.Responses
{
    public abstract record BaseResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }

        public BaseResponse(bool succeeded = true, string message = "", List<string> validationErrors = null)
        {
            Succeeded = succeeded;
            Message = message;
            ValidationErrors = validationErrors;
        }
    }
    
}
