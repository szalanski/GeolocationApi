using AutoMapper;
using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Exceptions;
using LanguageExt.Common;
using MediatR;
using System.Net;

namespace GeolocationApi.Application.Functions.Geolocations.Commands
{
    public record struct DeleteGeolocationCommand : IRequest<Result<string>>
    {
        public string Url { get; init; }
        public string Ip { get; init; }
    }

    public class DeleteGeolocationCommandHandler : IRequestHandler<DeleteGeolocationCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IGeolocationRepository _repository;

        public DeleteGeolocationCommandHandler(IMapper mapper, IGeolocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<string>> Handle(DeleteGeolocationCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetEntity(request, cancellationToken);
            if (entity == null) 
                return new Result<string>(new NotFoundException("Resource with provided IP address or Url cannot be found"));

            var result = await _repository.DeleteAsync(entity, cancellationToken);
            return new Result<string>(result.Ip);
        }

        private async Task<Geolocation> GetEntity(DeleteGeolocationCommand command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.Ip))
                return await _repository.GetByIpAsync(command.Ip, cancellationToken);


            if (!string.IsNullOrWhiteSpace(command.Url))
                return await _repository.GetByUrlAsync(command.Url, cancellationToken);

            return null;
        }
    }
}
