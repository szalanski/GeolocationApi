using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Functions.Geolocations.Queries;
using GeolocationApi.Application.Tests.Geolocations.Commands;
using System.Net;

namespace GeolocationApi.Application.Tests.Geolocations.Queries
{
    [TestClass]
    public  class GetGeolocationQueryTest : CommandQueryTestBase
    {
        [TestMethod]

        public async Task Handle_ShouldReadItemFromRepository_WhenValidIpAdressIsGiven()
        {
            //Arrange
            var ipAddress = "8.8.8.8";

            var repository = _repository.Object;
            var handler = new GetGeolocationQueryHandler(_mapper, repository);

            var item = await repository.GetByIpAsync(ipAddress,CancellationToken.None);
            var expectedResponse = _mapper.Map<GeolocationDto>(item);

            //Act
            var command = new GetGeolocationQuery
            {
                Ip = ipAddress
            };

            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsSuccess);

            response.IfSucc(response => Assert.AreEqual(expectedResponse, response));
            response.IfFail(error => Assert.Fail());
        }

        [TestMethod]
        public async Task Handle_ShouldReturnNotFound_WhenInvalidIpIsGiven()
        {
            //Arrange
            var ip = "127.0.0.1";

            var repository = _repository.Object;
            var handler = new GetGeolocationQueryHandler(_mapper, repository);

            var initialCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            //Act
            var command = new GetGeolocationQuery
            {
                Ip = ip
            };

            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsFaulted);
            var finalCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            Assert.AreEqual(initialCount, finalCount);
            response.IfSucc(response => Assert.Fail());
            response.IfFail(error =>
            {
                Assert.IsInstanceOfType(error, typeof(HttpRequestException));
                var httpException = (HttpRequestException)error;
                Assert.AreEqual("Resource with provided IP address or Url cannot be found", httpException.Message);
                Assert.AreEqual(HttpStatusCode.NotFound, httpException.StatusCode);
            });
        }
    }
}
