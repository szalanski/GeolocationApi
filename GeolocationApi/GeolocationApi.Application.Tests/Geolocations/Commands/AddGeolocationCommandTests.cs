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
