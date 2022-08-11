using AutoMapper;
using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Exceptions;
using LanguageExt.Common;
using MediatR;

namespace GeolocationApi.Application.Functions.Geolocations.Commands
{
    public record struct AddGeolocationCommand(string Input, bool IsIpAddress) : IRequest<Result<string>>;



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

        public async Task<Result<string>> Handle(AddGeolocationCommand request, CancellationToken ct)
        {
            var alreadyExists = await CheckIfExists(request, ct);
            if (alreadyExists)
                return new Result<string>(new AlreadyExistsException("Already exists"));

            var apiRequestResult = await _geolocationService.GetAsync(request.Input, ct);

            return await apiRequestResult.Match<Task<Result<string>>>(async result =>
            {
                var newLocation = _mapper.Map<Geolocation>(result);

                if (!request.IsIpAddress)
                    newLocation.Url = request.Input;

                var entity = await _repository.AddAsync(newLocation, ct);
                var dto = _mapper.Map<GeolocationDto>(entity);
                return new Result<string>(dto.Ip);
            },
            error => Task.FromResult(new Result<string>(error)));
        }

        private async Task<bool> CheckIfExists(AddGeolocationCommand command, CancellationToken ct)
        {
            if (command.IsIpAddress)
            {
                return (await _repository.GetByIpAsync(command.Input, ct)) is not null;
            }

            var result = await _repository.GetByUrlAsync(command.Input, ct);
            return result is not null;
        }
    }
}
