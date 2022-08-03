using GeolocationApi.Application.Models.GeolocationData;
using System.Net;

namespace GeolocationApi.Application.Responses
{
    public record GeolocationServiceResponse(GeolocationModel Content, HttpStatusCode StatusCode);
}
