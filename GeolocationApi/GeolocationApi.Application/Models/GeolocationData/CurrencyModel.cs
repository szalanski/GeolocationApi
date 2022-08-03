namespace GeolocationApi.Application.Models.GeolocationData
{
    public record CurrencyModel
    {
        public string code { get; set; }
        public string name { get; set; }
        public string plural { get; set; }
        public string symbol { get; set; }
        public string symbol_native { get; set; }
    }

}
