using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Responses;
using MediatR;
using System.Net;

namespace GeolocationApi.Application.Functions.Geolocations.Commands
{
    public record AddGeolocationCommandResponse : BaseResponse<GeolocationDto>
    {
        public AddGeolocationCommandResponse(bool succeeded = true, string message = "", GeolocationDto data = null, List<string> validationErrors = null)
            : base(succeeded, message, data, validationErrors)
        {

        }
        public string Ip { get; set; }
    }

    public record AddGeolocationCommand(string Url): IRequest<AddGeolocationCommandResponse>;
    

    public class AddGeolocationCommandHandler : IRequestHandler<AddGeolocationCommand, AddGeolocationCommandResponse>
    {
        private readonly IGeolocationRepository _repository;
        private readonly IGeolocationService _geolocationService;

        public AddGeolocationCommandHandler(IGeolocationRepository repository, IGeolocationService geolocationService)
        {
            _repository = repository;
            _geolocationService = geolocationService;
        }

        public async Task<AddGeolocationCommandResponse> Handle(AddGeolocationCommand request, CancellationToken cancellationToken)
        {
            //var apiResponse = await _geolocationService.GetAsync(request.Url, cancellationToken);
            //if (apiResponse.succeeded)
            //{

            //    return new AddGeolocationCommandResponse(true);
            //}


            //return new AddGeolocationCommandResponse(false, apiResponse.message, null);
            return null;
        }
    }
}
