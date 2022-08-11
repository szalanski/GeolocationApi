using AutoMapper;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Exceptions;
using LanguageExt.Common;
using MediatR;

namespace GeolocationApi.Application.Functions.Geolocations.Commands
{
    public record struct DeleteGeolocationCommand(string Ip) : IRequest<Result<string>>;

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
            var entity = await _repository.GetByIpAsync(request.Ip, cancellationToken);
            if (entity == null)
                return new Result<string>(new NotFoundException("Resource with provided IP address or Url cannot be found"));

            var result = await _repository.DeleteAsync(entity, cancellationToken);
            return new Result<string>(result.Ip);
        }
    }
}
