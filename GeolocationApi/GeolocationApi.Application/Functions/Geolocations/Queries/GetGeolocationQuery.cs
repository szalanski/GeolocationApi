using AutoMapper;
using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Dtos;
using LanguageExt.Common;
using MediatR;
using System.Net;

namespace GeolocationApi.Application.Functions.Geolocations.Queries
{
    public struct GetGeolocationQuery : IRequest<Result<GeolocationDto>>
    {
        public string Ip { get; set; }
        public string Url { get; set; }
    }

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
                return new Result<GeolocationDto>(new HttpRequestException("Resource with provided IP address or Url cannot be found", null, HttpStatusCode.NotFound));

            return new Result<GeolocationDto>(_mapper.Map<GeolocationDto>(entity));
        }

        private async Task<Geolocation> GetEntity(GetGeolocationQuery command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.Ip))
                return await _repository.GetByIpAsync(command.Ip, cancellationToken);


            if (!string.IsNullOrWhiteSpace(command.Url))
                return await _repository.GetByUrlAsync(command.Url, cancellationToken);

            return null;
        }
    }
}
