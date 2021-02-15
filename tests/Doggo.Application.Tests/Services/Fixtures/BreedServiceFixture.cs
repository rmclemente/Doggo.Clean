using Bogus;
using Doggo.Application.Dtos;
using Doggo.Application.Services;
using Doggo.Domain.Entities;
using Moq.AutoMock;

namespace Doggo.Application.Tests.Services.Fixtures
{
    public partial class ApplicationServiceFixture
    {
        public BreedDto GenerateValidBreedDto()
        {
            return new Faker<BreedDto>("pt_BR")
                .CustomInstantiator(c => new BreedDto
                {
                    Id = c.Random.Int(1, 1000),
                    UniqueId = c.Random.Guid(),
                    Name = c.Random.String2(10, 100),
                    Type = c.Random.String2(10, 100),
                    Family = c.Random.String2(10, 100),
                    Origin = c.Random.String2(10, 100),
                    OtherNames = c.Random.String2(10, 100)
                }).Generate();
        }

        public BreedDto GenerateInvalidBreedDto()
        {
            return new Faker<BreedDto>("pt_BR")
                .CustomInstantiator(c => new BreedDto
                {
                    Id = c.Random.Int(1, 1000),
                    UniqueId = c.Random.Guid(),
                    Name = c.Random.String2(101, 110),
                    Type = c.Random.String2(101, 110),
                    Family = c.Random.String2(101, 110),
                    Origin = c.Random.String2(101, 110),
                    OtherNames = c.Random.String2(101, 110)
                }).Generate();
        }

        public Breed GenerateValidBreed()
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

        public BreedService GetBreedService()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<BreedService>();
        }
    }
}
