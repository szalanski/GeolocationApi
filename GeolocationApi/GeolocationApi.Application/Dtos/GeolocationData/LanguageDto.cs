namespace GeolocationApi.Application.Dtos.GeolocationData
{
    public record LanguageDto
    {
        public string code { get; set; }
        public string name { get; set; }
        public string native { get; set; }
    }
}