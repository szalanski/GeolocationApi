using GeolocationApi.Application.Dtos.GeolocationData;
using System.Net;

namespace GeolocationApi.Application.Responses
{
    public record GeolocationServiceResponse(GeolocationDto Content, HttpStatusCode StatusCode);
}
