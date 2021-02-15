using Doggo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Doggo.Domain.Interfaces.Repository
{
    public interface IBreedRepository : IRepository<Breed>
    {
        void Add(Breed entity);
        Task<bool> Any(Expression<Func<Breed, bool>> where);
        Task<IEnumerable<Breed>> GetAll(bool asNoTracking = true);
        Task<Breed> Get(int id, bool asNoTracking = true);
        Task<Breed> Get(Guid UniqueId, bool asNoTracking = true);
        void Update(Breed entity);
    }
}
