using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Functions.Geolocations.Commands;
using GeolocationApi.Application.Models.GeolocationData;
using GeolocationApi.Application.Services;
using GeolocationApi.Application.Tests.Mock;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace GeolocationApi.Application.Tests.Geolocations.Commands
{
    [TestClass]
    public class AddGeolocationCommandTests
    {

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

        private readonly Mock<IGeolocationRepository> _repository;

        [TestMethod]
        public async Task Handle_ShouldAddNewItemToRepository_WhenValidIpAdressIsGiven()
        {
            //Arrange
            
            var ipAddress = "8.8.8.8";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(TestJson, Encoding.UTF8, "application/json")
            };

            var geolocationService = CreateGeolocationService(expectedResponse);

            var repository = _repository.Object;
            var expectedRespone = repository.GetByIpAsync(ipAddress);

            var handler = new AddGeolocationCommandHandler(repository, geolocationService);

            //Act
            var command = new AddGeolocationCommand(ipAddress);
            var response = await handler.Handle(command, CancellationToken.None);


        }

        private static IGeolocationService CreateGeolocationService(HttpResponseMessage response)
        {
            var client = HttpClientMock.GetClient(response);
            return new GeolocationService(client, "");
        }
    }
}
