using Application.Requests.Breeds.Mappers;
using Domain.Core.SeedWork;
using Domain.Interfaces;
using MediatR;

namespace Application.Requests.Breeds.Queries
{
    public class GetPaginatedBreedQueryHandler : IRequestHandler<GetPaginatedBreedQuery, QueryResult>
    {
        private readonly IBreedRepository _breedRepository;

        public GetPaginatedBreedQueryHandler(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<QueryResult> Handle(GetPaginatedBreedQuery request, CancellationToken cancellationToken)
        {
            var result = await _breedRepository.GetAll(request.Skip, request.Take, p => p.Id, false, true, cancellationToken);
            var pagedResult = result.ToResponse(request.Page, request.PerPage);
            return QueryResult.SuccessResult().WithContent(pagedResult);
        }
    }

    public class GetPaginatedBreedQuery : PaginatedQuery
    {
        public GetPaginatedBreedQuery(int page, int rows) : base(page, rows)
        {
        }
    }
}
