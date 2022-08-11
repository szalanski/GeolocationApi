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
    public struct GetAllGeolocationsQuery : IRequest<Result<IEnumerable<GeolocationDto>>> { }


    public class GetAllGeolocationsQueryHandler : IRequestHandler<GetAllGeolocationsQuery, Result<IEnumerable<GeolocationDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IGeolocationRepository _repository;

        public GetAllGeolocationsQueryHandler(IMapper mapper, IGeolocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<IEnumerable<GeolocationDto>>> Handle(GetAllGeolocationsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllAsync(cancellationToken);
            if (entity == null)
                return new Result<IEnumerable<GeolocationDto>>(new NotFoundException("Resources with provided IP address or Url cannot be found"));

            return new Result<IEnumerable<GeolocationDto>>(_mapper.Map<IEnumerable<Geolocation>, IEnumerable<GeolocationDto>>(entity));
        }
    }
}
