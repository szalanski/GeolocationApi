using AutoMapper;
using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Dtos;
using LanguageExt.Common;
using MediatR;

namespace GeolocationApi.Application.Functions.Geolocations.Commands
{
    public record struct AddGeolocationCommand(string Url) : IRequest<Result<string>>;

    public class AddGeolocationCommandHandler : IRequestHandler<AddGeolocationCommand, Result<string>>
    {
        private readonly IGeolocationRepository _repository;
        private readonly IGeolocationService _geolocationService;
        private readonly IMapper _mapper;

        public AddGeolocationCommandHandler(IGeolocationRepository repository, IGeolocationService geolocationService, IMapper mapper)
        {
            _repository = repository;
            _geolocationService = geolocationService;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(AddGeolocationCommand request, CancellationToken cancellationToken)
        {
            var apiRequestResult = await _geolocationService.GetAsync(request.Url, cancellationToken);

            return await apiRequestResult.Match<Task<Result<string>>>(async result =>
            {
                var newLocation = _mapper.Map<Geolocation>(result);
                var entity = await _repository.AddAsync(newLocation, cancellationToken);
                var dto = _mapper.Map<GeolocationDto>(entity);
                return new Result<string>(dto.Ip);
            },
            error => Task.FromResult(new Result<string>(error)));
        }
    }
}
