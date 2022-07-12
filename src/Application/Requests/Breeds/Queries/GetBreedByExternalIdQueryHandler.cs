using Application.Requests.Breeds.Mappers;
using Domain.Core.SeedWork;
using Domain.Interfaces;
using MediatR;

namespace Application.Requests.Breeds.Queries
{
    public class GetBreedByExternalIdQueryHandler : IRequestHandler<GetBreedByExternalIdQuery, QueryResult>
    {
        private readonly IBreedRepository _breedRepository;

        public GetBreedByExternalIdQueryHandler(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<QueryResult> Handle(GetBreedByExternalIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _breedRepository.Get(cancellationToken: cancellationToken, where: p => p.ExternalId == request.ExternalId);
            var result = entity?.ToBriefResponse();
            return QueryResult.ResultValidatedResult(result);
        }
    }

    public class GetBreedByExternalIdQuery : Query
    {
        public Guid ExternalId { get; private set; }

        public GetBreedByExternalIdQuery(Guid externalId)
        {
            ExternalId = externalId;
        }
    }
}
