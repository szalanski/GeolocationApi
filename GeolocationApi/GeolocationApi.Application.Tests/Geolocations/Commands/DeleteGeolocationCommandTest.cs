using GeolocationApi.Application.Exceptions;
using GeolocationApi.Application.Functions.Geolocations.Commands;
using System.Net;

namespace GeolocationApi.Application.Tests.Geolocations.Commands
{

    [TestClass]
    public class DeleteGeolocationCommandTest : CommandQueryTestBase
    {
        [TestMethod]
        public async Task Handle_ShouldDeleteItemFromRepository_WhenValidIpAdressIsGiven()
        {
            //Arrange
            var ipAddress = "8.8.8.8";

            var repository = _repository.Object;
            var handler = new DeleteGeolocationCommandHandler(_mapper, repository);

            var initialCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            //Act
            var command = new DeleteGeolocationCommand(ipAddress);

            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsSuccess);
            var finalCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            Assert.AreEqual(initialCount - 1, finalCount);
            response.IfSucc(response => Assert.AreEqual(ipAddress, response));
            response.IfFail(error => Assert.Fail());
        }

        [TestMethod]
        public async Task Handle_ShouldReturnNotFound_WhenInvalidIpIsGiven()
        {
            //Arrange
            var ip = "127.0.0.1";

            var repository = _repository.Object;
            var handler = new DeleteGeolocationCommandHandler(_mapper, repository);

            var initialCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            //Act
            var command = new DeleteGeolocationCommand(ip);

            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsFaulted);
            var finalCount = repository.GetAllAsync(CancellationToken.None).GetAwaiter().GetResult().Count;

            Assert.AreEqual(initialCount, finalCount);
            response.IfSucc(response => Assert.Fail());
            response.IfFail(error =>
            {
                Assert.IsInstanceOfType(error, typeof(NotFoundException));
                Assert.AreEqual("Resource with provided IP address or Url cannot be found", error.Message);
            });
        }
    }
}

