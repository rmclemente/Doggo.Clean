using Domain.Entities;
using Domain.Seedwork;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IBreedRepository : IRepository<Breed>
    {
        void Add(Breed entity);
        Task<Breed> Get(bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<Breed, bool>>[] where);
        Task<Tuple<IEnumerable<Breed>, int>> GetAll(int skip, int take, Expression<Func<Breed, object>> orderBy, bool asc = true, bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<Breed, bool>>[] where);
    }
}