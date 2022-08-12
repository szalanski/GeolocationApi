using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Exceptions;
using GeolocationApi.Application.Functions.Geolocations.Commands;
using GeolocationApi.Application.Services;
using GeolocationApi.Application.Tests.Mock;
using System.Net;
using System.Text;

namespace GeolocationApi.Application.Tests.Geolocations.Commands
{
    [TestClass]
    public class AddGeolocationCommandTests : CommandQueryTestBase
    {

        [TestMethod]
        public async Task Handle_ShouldAddNewItemToRepository_WhenValidIpAdressIsGiven()
        {
            //Arrange
            var ipAddress = "185.21.87.139";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(TestJson, Encoding.UTF8, "application/json")
            };

            var expectedItem = new GeolocationDto
            {
                Ip = ipAddress,
                Type = "ipv4",
                ContinentCode = "EU",
                ContinentName = "Europe",
                CountryCode = "PL",
                CountryName = "Poland",
                RegionCode = "ZP",
                RegionName = "West Pomerania",
                City = "Koszalin",
                Zip = "76-024",
                Latitude = 54.121421813964844,
                Longitude = 16.168630599975586
            };

            var geolocationService = CreateGeolocationService(expectedResponse);

            var repository = _repository.Object;
            var initialCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;
            var handler = new AddGeolocationCommandHandler(repository, geolocationService, _mapper);

            //Act
            var command = new AddGeolocationCommand(ipAddress, true);
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsSuccess);
            var finalCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;
            var addedItem = await repository.GetByIpAsync(ipAddress, CancellationToken.None);

            Assert.AreEqual(expectedItem, _mapper.Map<GeolocationDto>(addedItem));
            Assert.AreEqual(initialCount + 1, finalCount);
            response.IfSucc(response => Assert.AreEqual(ipAddress, response));
            response.IfFail(error => Assert.Fail());
        }


        [TestMethod]
        public async Task Handle_ShouldAlreadysExistEerror_WhenAddingItemWhichAlreadyExists()
        {
            var ipAddress = "8.8.8.8";

            var responseJson = @"   {
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

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
            };

            var geolocationService = CreateGeolocationService(expectedResponse);

            var repository = _repository.Object;
            var initialCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;
            var handler = new AddGeolocationCommandHandler(repository, geolocationService, _mapper);

            //Act
            var command = new AddGeolocationCommand(ipAddress, true);
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsFaulted);
            var finalCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            Assert.AreEqual(initialCount, finalCount);
            response.IfSucc(response => Assert.Fail());
            response.IfFail(error =>
            {
                Assert.IsInstanceOfType(error, typeof(AlreadyExistsException));
            });
        }


        [TestMethod]
        public async Task Handle_ShouldReturnInternalEerror_WhenApiResponseWithBadRequest()
        {
            //Arrange
            var ipAddress = "";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            var geolocationService = CreateGeolocationService(expectedResponse);

            var repository = _repository.Object;
            var initialCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;
            var handler = new AddGeolocationCommandHandler(repository, geolocationService, _mapper);

            //Act
            var command = new AddGeolocationCommand(ipAddress, true);
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsFaulted);
            var finalCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            Assert.AreEqual(initialCount, finalCount);
            response.IfSucc(response => Assert.Fail());
            response.IfFail(error =>
            {
                Assert.IsInstanceOfType(error, typeof(InternalErrorException));
            });
        }

        private static IGeolocationService CreateGeolocationService(HttpResponseMessage response)
        {
            var client = HttpClientMock.GetClient(response);
            return new GeolocationService(client, "");
        }
    }
}
