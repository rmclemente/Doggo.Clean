using Application.Extensions;
using Application.Requests.Breeds.Responses;
using Application.SeedWork;
using Domain.Entities;

namespace Application.Requests.Breeds.Mappers
{
    public static class BreedResponseMapper
    {
        public static BreedBriefResponse ToBriefResponse(this Breed source)
            => new(source.ExternalId,
                   source.Name,
                   source.BreedType?.ToEnumerationResponse(),
                   source.Family,
                   source.Origin);

        public static BreedResponse ToResponse(this Breed source)
            => new(source.ExternalId,
                   source.Name,
                   source.BreedType?.ToEnumerationResponse(),
                   source.Family,
                   source.Origin,
                   source.CreatedAt?.LocalDateTime,
                   source.CreatedBy,
                   source.LastUpdateAt?.LocalDateTime,
                   source.LastUpdateBy);

        public static IEnumerable<BreedResponse> ToResponse(this IEnumerable<Breed> source)
            => source.Select(p => p.ToResponse());

        public static PaginatedResponse<BreedResponse> ToResponse(this Tuple<IEnumerable<Breed>, int> source, int page, int perPage)
            => new(source.Item1.ToResponse(), page, perPage, source.Item2);
    }
}
