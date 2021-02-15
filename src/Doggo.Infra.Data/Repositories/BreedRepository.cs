using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace Doggo.Infra.Data.Repositories.Parametrizacao
{
    public class BreedRepository : Repository<Breed>, IBreedRepository
    {
        private readonly DoggoCleanContext _context;

        public BreedRepository(DoggoCleanContext context) : base(context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Breed> Get(int id, bool asNoTracking = true)
        {
            return await base.Get(p => p.Id == id, asNoTracking);
        }

        public async Task<Breed> Get(Guid uniqueId, bool asNoTracking = true)
        {
            return await base.Get(p => p.UniqueId == uniqueId, asNoTracking);
        }
    }
}
