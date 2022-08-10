using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Models.GeolocationData;
using GeolocationApi.Application.Services;
using GeolocationApi.Application.Tests.Mock;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace GeolocationApi.Application.Tests.Services
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


        [TestMethod]
        public async Task GetAsync_ShouldReturnGeolocationDto_WhenValidIpAddressIsGiven()
        {
            //Arrange
            var expectedContent = JsonConvert.DeserializeObject<GeolocationModel>(TestJson);
            var ipAddress = "8.8.8.8";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(TestJson, Encoding.UTF8, "application/json")
            };

            using var service = CreateServiceUnderTest(expectedResponse);

            //Act
            var response = await service.GetAsync(ipAddress, CancellationToken.None);


            //Assert
            Assert.IsTrue(response.IsSuccess);
            response.IfSucc(r => Assert.AreEqual(expectedContent, r));
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
            var response = await service.GetAsync(url, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsSuccess);
            response.IfSucc(r => Assert.AreEqual(expectedContent, r));
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnBadRequestAndSucceesFalse_WhenApiReturnsBadRequest()
        {
            //Arrange
            var ipAddress = "8.8.8.8";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
            };
            using var service = CreateServiceUnderTest(expectedResponse);

            //Act
            var response = await service.GetAsync(ipAddress, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsFaulted);
            response.IfFail(exception =>
            {
                Assert.IsInstanceOfType(exception, typeof(HttpRequestException));
                Assert.IsTrue(((HttpRequestException)exception).StatusCode == HttpStatusCode.BadRequest);
            });
        }

        [TestMethod]
        public async Task GetAsync_ShdouldReturnSucceddedFalse_WhenApiRespondsWithErrorMessage()
        {
            //Arrange
            string ipAddress = "";

            var error = new
            {
                success = false,
                error = new
                {
                    code = 106,
                    type = "invalid_ip_address",
                    info = "The IP Address supplied is invalid."
                }
            };

            var json = JsonConvert.SerializeObject(error);

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            using var service = CreateServiceUnderTest(expectedResponse);
            //Act
            var response = await service.GetAsync(ipAddress, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsFaulted);
            response.IfFail(exception =>
            {
                Assert.IsInstanceOfType(exception, typeof(HttpRequestException));
                Assert.IsTrue(((HttpRequestException)exception).StatusCode == HttpStatusCode.BadRequest);
            });
        }


        private IGeolocationService CreateServiceUnderTest(HttpResponseMessage response)
        {
            var client = HttpClientMock.GetClient(response);
            return new GeolocationService(client, ApiKey);
        }

    }
}