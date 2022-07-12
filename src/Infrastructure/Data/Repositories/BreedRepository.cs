using Domain.Entities;
using Domain.Interfaces;
using Domain.Seedwork;
using Infrastructure.Data.Context;
using Infrastructure.Data.SeedWork;

namespace Infrastructure.Data.Repositories
{
    public class BreedRepository : Repository<Breed>, IBreedRepository
    {
        public BreedRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => Db;
    }
}
