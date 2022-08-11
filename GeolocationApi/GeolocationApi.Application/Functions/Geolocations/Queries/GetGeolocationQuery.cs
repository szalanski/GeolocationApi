using AutoMapper;
using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Exceptions;
using LanguageExt.Common;
using MediatR;
using System.Net;

namespace GeolocationApi.Application.Functions.Geolocations.Queries
{
    public record struct GetGeolocationQuery(string Input, bool isIpAddress) : IRequest<Result<GeolocationDto>>;

    public class GetGeolocationQueryHandler : IRequestHandler<GetGeolocationQuery, Result<GeolocationDto>>
    {

        private readonly IMapper _mapper;
        private readonly IGeolocationRepository _repository;

        public GetGeolocationQueryHandler(IMapper mapper, IGeolocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<GeolocationDto>> Handle(GetGeolocationQuery request, CancellationToken cancellationToken)
        {
            var entity = await GetEntity(request, cancellationToken);
            if (entity == null)
                return new Result<GeolocationDto>(new NotFoundException("Resource with provided IP address or Url cannot be found"));

            return new Result<GeolocationDto>(_mapper.Map<GeolocationDto>(entity));
        }

        private async Task<Geolocation> GetEntity(GetGeolocationQuery command, CancellationToken cancellationToken)
        {
            if (command.isIpAddress)
            {
                return await _repository.GetByIpAsync(command.Input, cancellationToken);
            }

            return await _repository.GetByUrlAsync(command.Input, cancellationToken);
        }
    }
}
