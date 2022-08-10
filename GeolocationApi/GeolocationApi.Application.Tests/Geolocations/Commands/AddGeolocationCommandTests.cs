using AutoMapper;
using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Functions.Geolocations.Commands;
using GeolocationApi.Application.Services;
using GeolocationApi.Application.Tests.Mock;
using Moq;
using System.Net;
using System.Text;

namespace GeolocationApi.Application.Tests.Geolocations.Commands
{
    [TestClass]
    public class AddGeolocationCommandTests
    {

        private const string TestJson = @"{
                                            ""ip"": ""185.21.87.139"",
                                            ""type"": ""ipv4"",
                                            ""continent_code"": ""EU"",
                                            ""continent_name"": ""Europe"",
                                            ""country_code"": ""PL"",
                                            ""country_name"": ""Poland"",
                                            ""region_code"": ""ZP"",
                                            ""region_name"": ""West Pomerania"",
                                            ""city"": ""Koszalin"",
                                            ""zip"": ""76-024"",
                                            ""latitude"": 54.121421813964844,
                                            ""longitude"": 16.168630599975586,
                                            ""location"": {
                                                ""geoname_id"": 3095049,
                                                ""capital"": ""Warsaw"",
                                                ""languages"": [
                                                    {
                                                        ""code"": ""pl"",
                                                        ""name"": ""Polish"",
                                                        ""native"": ""Polski""
                                                    }
                                                ],
                                                ""country_flag"": ""https://assets.ipstack.com/flags/pl.svg"",
                                                ""country_flag_emoji"": ""🇵🇱"",
                                                ""country_flag_emoji_unicode"": ""U+1F1F5 U+1F1F1"",
                                                ""calling_code"": ""48"",
                                                ""is_eu"": true
                                            }
                                        }";

        private readonly Mock<IGeolocationRepository> _repository;
        private readonly IMapper _mapper;

        public AddGeolocationCommandTests()
        {
            _repository = RepositoryMocks.GetGeolocationRepository();

            var cfgProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = cfgProvider.CreateMapper();
        }

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
            var initialCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;
            var handler = new AddGeolocationCommandHandler(repository, geolocationService, _mapper);

            //Act
            var command = new AddGeolocationCommand(ipAddress);
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsSuccess);
            var finalCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;
            var addedItem = await repository.GetByIpAsync(ipAddress);

            Assert.AreEqual(expectedItem, _mapper.Map<GeolocationDto>(addedItem));
            Assert.AreEqual(initialCount + 1, finalCount);
            response.IfSucc(response => Assert.AreEqual(ipAddress, response));
            response.IfFail(error => Assert.Fail());
        }


        [TestMethod]
        public async Task Handle_ShouldHttpRequestException_WhenStringEmptyIpAdressIsGiven()
        {
            //Arrange
            var ipAddress = "";

            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            var geolocationService = CreateGeolocationService(expectedResponse);

            var repository = _repository.Object;
            var initialCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;
            var handler = new AddGeolocationCommandHandler(repository, geolocationService, _mapper);

            //Act
            var command = new AddGeolocationCommand(ipAddress);
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsFaulted);
            var finalCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

            Assert.AreEqual(initialCount, finalCount);
            response.IfSucc(response => Assert.Fail());
            response.IfFail(error =>
            {
                Assert.IsInstanceOfType(error, typeof(HttpRequestException));
                Assert.IsTrue(((HttpRequestException)error).StatusCode == HttpStatusCode.BadRequest);
            });
        }

        private static IGeolocationService CreateGeolocationService(HttpResponseMessage response)
        {
            var client = HttpClientMock.GetClient(response);
            return new GeolocationService(client, "");
        }
    }
}
