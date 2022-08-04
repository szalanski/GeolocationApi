using GeolocationApi.Application.Models.GeolocationData;
using System.Net;

namespace GeolocationApi.Application.Responses
{
    public record GeolocationServiceResponse(GeolocationModel content, HttpStatusCode statusCode, bool succeeded = true, string message ="");

}
