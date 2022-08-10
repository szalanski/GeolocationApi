using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Functions.Geolocations.Queries;
using GeolocationApi.Application.Tests.Geolocations.Commands;

namespace GeolocationApi.Application.Tests.Geolocations.Queries
{
    [TestClass]
    public class GetAllGeolocationsQueryTest : CommandQueryTestBase
    {
        [TestMethod]
        public async Task Handle_ShouldReadAllItemsFromRepository_WhenValidIpAdressIsGiven()
        {
            //Arrange
            var repository = _repository.Object;
            var handler = new GetAllGeolocationsQueryHandler(_mapper, repository);

            var items = await repository.GetAllAsync(CancellationToken.None);
            var expectedResponse = _mapper.Map<IEnumerable<GeolocationDto>>(items);

            //Act
            var command = new GetAllGeolocationsQuery();
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.IsSuccess);

            response.IfSucc(response => 
            {
                Assert.AreEqual(expectedResponse.Count(), response.Count());
                CollectionAssert.AreEqual(expectedResponse.ToList(), response.ToList());
            });
            response.IfFail(error => Assert.Fail());
        }
    }
}
