using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

#pragma warning disable CS8604 // Possible null reference argument.
namespace Infrastructure.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AddFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> clause, int? propertyValue)
        {
            if (propertyValue is not null && propertyValue.Value != default)
                query = query.Where(clause);

            return query;
        }

        public static IQueryable<T> AddFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> clause, Guid? propertyValue)
        {
            if (propertyValue is not null && propertyValue.Value != default)
                query = query.Where(clause);

            return query;
        }

        public static IQueryable<T> AddFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> clause, DateTime? propertyValue)
        {
            if (propertyValue is not null && propertyValue.Value != default)
                query = query.Where(clause);

            return query;
        }

        public static IQueryable<T> AddFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> clause, string propertyValue)
        {
            if (!string.IsNullOrWhiteSpace(propertyValue))
                query = query.Where(clause);

            return query;
        }

        public static IQueryable<T> AddRange<T>(this IQueryable<T> query, int skip, int take)
        {
            query = query.Skip(skip);
            query = query.Take(take);

            return query;
        }

        public static IQueryable<T> AddOrderby<T>(this IQueryable<T> query, string orderBy, bool asc = true)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderByDescending(p => EF.Property<string>(p, "Id"));
                return query;
            }

            if (asc)
                query = query.OrderBy(p => EF.Property<string>(p, orderBy));
            else
                query = query.OrderByDescending(p => EF.Property<string>(p, orderBy));

            return query;
        }
    }
}