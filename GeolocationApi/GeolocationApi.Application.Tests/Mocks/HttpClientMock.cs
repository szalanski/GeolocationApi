using System.Net.Http.Headers;

namespace GeolocationApi.Application.Tests.Mock
{
    public static class HttpClientMock
    {
        public static HttpClient GetClient(HttpResponseMessage response)
        {

            var client = new HttpClient(new HttpMessageHandlerMock(response));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;

        }
    }
}