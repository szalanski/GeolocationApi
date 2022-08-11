using GeolocationApi.Api.Extensions;
using GeolocationApi.Application.Functions.Geolocations.Commands;
using GeolocationApi.Application.Functions.Geolocations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeolocationApi.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GeolocationsController : ControllerBase
    {
        private readonly ILogger<GeolocationsController> _logger;
        private readonly IMediator _mediator;

        public GeolocationsController(ILogger<GeolocationsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var response = await _mediator.Send(new GetAllGeolocationsQuery(), ct);
            return response.Unwrap(r => r.ToArray());
        }

        [HttpGet("{input}")]
        public async Task<IActionResult> GetByIp(string input, [FromQuery] bool isIpAddress = true)
        {
            var response = await _mediator.Send(new GetGeolocationQuery(input, isIpAddress));
            return response.Unwrap(r => r);
        } 

        [HttpPost]
        public async Task<IActionResult> AddIp([FromBody] AddGeolocationCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Unwrap(r => new { Ip = r });
        }

        [HttpDelete("{ip}")]
        public async Task<IActionResult> Delete(string ip)
        {
            var response = await _mediator.Send(new DeleteGeolocationCommand(ip));
            return response.Unwrap(r => new { Ip = r });
        }

    }
}