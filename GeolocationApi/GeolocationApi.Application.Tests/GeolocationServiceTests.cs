using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Dtos.GeolocationData;
using GeolocationApi.Application.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GeolocationApi.Application.Tests
{
    [TestClass]
    public class GeolocationServiceTests
    {
        private const string ApiKey = "";
        private const string TestJson = @"{
                                            ""ip"": ""8.8.8.8"",
                                            ""type"": ""ipv4"",
                                            ""continent_code"": ""NA"",
                                            ""continent_name"": ""North America"",
                                            ""country_code"": ""US"",
                                            ""country_name"": ""United States"",
                                            ""region_code"": ""OH"",
                                            ""region_name"": ""Ohio"",
                                            ""city"": ""Glenmont"",
                                            ""zip"": ""44628"",
                                            ""latitude"": 40.5369987487793,
                                            ""longitude"": -82.12859344482422,
                                            ""location"": {
                                                ""geoname_id"": null,
                                                ""capital"": ""Washington D.C."",
                                                ""languages"": [
                                                    {
                                                        ""code"": ""en"",
                                                        ""name"": ""English"",
                                                        ""native"": ""English""
                                                    }
                                                ],
                                                ""country_flag"": ""https://assets.ipstack.com/flags/us.svg"",
                                                ""country_flag_emoji"": ""🇺🇸"",
                                                ""country_flag_emoji_unicode"": ""U+1F1FA U+1F1F8"",
                                                ""calling_code"": ""1"",
                                                ""is_eu"": false
                                            }
                                        }";

        private IGeolocationService _service;
        public GeolocationServiceTests()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _service = new GeolocationService(client, ApiKey);
        }


        [TestMethod]
        public async Task GetAsync_ShouldReturnGeolocationDto_WhenValidIpAddressIsGiven()
        {
            var expectedResponse = JsonConvert.DeserializeObject<GeolocationDto>(TestJson);
            var ipAddress = "8.8.8.8";


            var response = await _service.GetAsync(ipAddress);
            Assert.AreEqual(expectedResponse, response);
        }
    }
}