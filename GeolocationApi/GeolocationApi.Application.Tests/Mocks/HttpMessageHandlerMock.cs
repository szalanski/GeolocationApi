namespace GeolocationApi.Application.Tests.Mock
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
}