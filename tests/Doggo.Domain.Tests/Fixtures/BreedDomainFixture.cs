using Bogus;
using Doggo.Domain.Entities;

namespace Doggo.Domain.Tests.Fixtures
{
    public partial class DomainFixture
    {
        public Breed GenerateValidBreed(bool enabled = true)
        {
            return new Faker<Breed>("pt_BR")
                        .CustomInstantiator(c => new Breed(c.Random.String2(10, 100), c.Random.String2(10, 100), c.Random.String2(10, 100), c.Random.String2(10, 100), null, c.Random.String2(10, 100)))
                        .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                        .Generate();
        }

        public Breed GenerateInvalidBreed()
        {
            return new Faker<Breed>("pt_BR")
                .CustomInstantiator(c => new Breed(c.Random.String2(101, 110), c.Random.String2(101, 110), c.Random.String2(101, 110), c.Random.String2(101, 110), null, c.Random.String2(101, 110)))
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .Generate();
        }
    }
}
