using Application.SeedWork;
using Domain.Seedwork;

namespace Application.Extensions
{
    public static partial class MapperExtensions
    {
        public static EnumerationResponse ToEnumerationResponse<TEnum>(this Enumeration<TEnum> source) where TEnum : Enumeration<TEnum, int>
            => new EnumerationResponse(source.Id, source.Name);

        public static IEnumerable<EnumerationResponse> ToEnumerationResponse<TEnum>(this IEnumerable<Enumeration<TEnum>> source) where TEnum : Enumeration<TEnum, int>
            => source.Select(p => p.ToEnumerationResponse());
    }
}
