using GeolocationApi.Application.Functions.Geolocations.Commands;
using System.Net;

namespace GeolocationApi.Application.Tests.Geolocations.Commands
{
    public abstract partial class CommandTestBase
    {
        [TestClass]
        public class DeleteGeolocationCommandTest : CommandTestBase
        {
            [TestMethod]
            public async Task Handle_ShouldDeleteItemFromRepository_WhenValidIpAdressIsGiven()
            {
                //Arrange
                var ipAddress = "8.8.8.8";

                var repository = _repository.Object;
                var handler = new DeleteGeolocationCommandHandler(_mapper, repository);

                var initialCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

                //Act
                var command = new DeleteGeolocationCommand
                {
                    Ip = ipAddress
                };

                var response = await handler.Handle(command, CancellationToken.None);

                //Assert
                Assert.IsTrue(response.IsSuccess);
                var finalCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

                Assert.AreEqual(initialCount - 1, finalCount);
                response.IfSucc(response => Assert.AreEqual(ipAddress, response));
                response.IfFail(error => Assert.Fail());
            }


            [TestMethod]
            public async Task Handle_ShouldDeleteItemFromRepository_WhenValidUrlIsGiven()
            {
                //Arrange
                var url = "www.google.com";
                var ipAddess = "8.8.8.8";

                var repository = _repository.Object;
                var handler = new DeleteGeolocationCommandHandler(_mapper, repository);

                var initialCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

                //Act
                var command = new DeleteGeolocationCommand
                {
                    Url = url
                };

                var response = await handler.Handle(command, CancellationToken.None);

                //Assert
                Assert.IsTrue(response.IsSuccess);
                var finalCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

                Assert.AreEqual(initialCount - 1, finalCount);
                response.IfSucc(response => Assert.AreEqual(ipAddess, response));
                response.IfFail(error => Assert.Fail());
            }

            [TestMethod]
            public async Task Handle_ShouldReturnNotFound_WhenInvalidUrlIsGiven()
            {
                //Arrange
                var url = "www.wp.pl";

                var repository = _repository.Object;
                var handler = new DeleteGeolocationCommandHandler(_mapper, repository);

                var initialCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

                //Act
                var command = new DeleteGeolocationCommand
                {
                    Url = url
                };

                var response = await handler.Handle(command, CancellationToken.None);

                //Assert
                Assert.IsTrue(response.IsFaulted);
                var finalCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

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


            [TestMethod]
            public async Task Handle_ShouldReturnNotFound_WhenInvalidIpIsGiven()
            {
                //Arrange
                var ip = "127.0.0.1";

                var repository = _repository.Object;
                var handler = new DeleteGeolocationCommandHandler(_mapper, repository);

                var initialCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

                //Act
                var command = new DeleteGeolocationCommand
                {
                    Ip = ip
                };

                var response = await handler.Handle(command, CancellationToken.None);

                //Assert
                Assert.IsTrue(response.IsFaulted);
                var finalCount = repository.GetAllAsync().GetAwaiter().GetResult().Count;

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
}
