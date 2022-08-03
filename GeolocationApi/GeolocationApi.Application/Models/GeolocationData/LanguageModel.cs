namespace GeolocationApi.Application.Models.GeolocationData
{
    public record LanguageModel
    {
        public string code { get; set; }
        public string name { get; set; }
        public string native { get; set; }
    }
}