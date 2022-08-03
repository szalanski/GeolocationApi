using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Models.GeolocationData;
using GeolocationApi.Application.Services;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace GeolocationApi.Application.Tests
{
    [TestClass]
    public class GeolocationServiceTests
    {
        public class HttpMessageHandlerMock : HttpMessageHandler
        {
            private readonly HttpResponseMessage _response;

            public HttpMessageHandlerMock(HttpResponseMessage respone)
            {
                _response = respone;
            }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_response);
            }
        }

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


        [TestMethod]
        public async Task GetAsync_ShouldReturnGeolocationDto_WhenValidIpAddressIsGiven()
        {
            //Arrange
            var expectedContent = JsonConvert.DeserializeObject<GeolocationModel>(TestJson);
            var ipAddress = "8.8.8.8";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(TestJson, Encoding.UTF8, "application/json")
            };

            using var service = CreateServiceUnderTest(expectedResponse);

            //Act
            var response = await service.GetAsync(ipAddress);

            //Assert
            Assert.AreEqual(expectedContent, response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        public async Task GetAsync_ShouldReturnGeolocationDto_WhenValidUrlAddressIsGiven()
        {
            //arrange
            var expectedContent = JsonConvert.DeserializeObject<GeolocationModel>(TestJson);
            var url = "www.google.com";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(TestJson, Encoding.UTF8, "application/json")
            };
            using var service = CreateServiceUnderTest(expectedResponse);

            //Act
            var response = await service.GetAsync(url);

            //Assert
            Assert.AreEqual(expectedContent, response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_ShouldThrowException_WhenBadRequestStatusIsReturned()
        {
            //Arrange
            var ipAddress = "8.8.8.8";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
            };
            using var service = CreateServiceUnderTest(expectedResponse);

            //Act
            var response = await service.GetAsync(ipAddress);

            //Assert
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private IGeolocationService CreateServiceUnderTest(HttpResponseMessage response)
        {
            var client = new HttpClient(new HttpMessageHandlerMock(response));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return new GeolocationService(client, ApiKey);
        }

    }
}