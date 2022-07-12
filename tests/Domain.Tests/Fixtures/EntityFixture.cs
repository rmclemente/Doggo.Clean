using Ardalis.GuardClauses;
using Bogus;
using Domain.Core.SeedWork;
using Domain.Extensions;
using Domain.Seedwork;

namespace Domain.Tests.Fixtures
{
    public partial class DomainFixture
    {
        public static Foo GenerateFoo(int nameLength = 50, bool isTransient = false)
        {
            var faker = new Faker<Foo>("pt_BR")
                .CustomInstantiator(c => new Foo(c.Random.String2(nameLength), c.Random.Int(18, 65)));

            if (!isTransient)
                faker.RuleFor(p => p.Id, f => f.Random.Int(1, 1000));

            return faker.Generate();
        }

        public static Bar GenerateBar(int descriptionLength = 50, bool isTransient = false)
        {
            var faker = new Faker<Bar>("pt_BR")
                .CustomInstantiator(c => new Bar(c.Random.String2(descriptionLength), c.Date.Past(1)));

            if (!isTransient)
                faker.RuleFor(p => p.Id, f => f.Random.Int(1, 1000));

            return faker.Generate();
        }

        public static FooDomainEvent GenerateFooDomainEvent()
        {
            return new FooDomainEvent(Guid.NewGuid());
        }

        public class Foo : Entity
        {
            public string Name { get; private set; }
            public int Age { get; private set; }

            public Foo(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public override void Validate()
            {
                Guard.Against.NullOrEmpty(Name, nameof(Name));
                Guard.Against.OutOfLength(Name, nameof(Name), 3, 100);
                Guard.Against.OutOfRange(Age, nameof(Age), 18, 65);
            }

            public override IEnumerable<string> GetEqualityProperties()
            {
                yield return nameof(Name);
                yield return nameof(Age);
            }
        }

        public class Bar : Entity, ITogglable
        {
            public string Description { get; private set; }
            public DateTime IssueDate { get; private set; }
            public bool Enabled { get; private set; }

            public Bar(string description, DateTime issueDate, bool enabled = true)
            {
                Description = description;
                IssueDate = issueDate;
                Enabled = enabled;
            }

            public override void Validate()
            {
                Guard.Against.NullOrEmpty(Description, nameof(Description));
                Guard.Against.OutOfLength(Description, nameof(Description), 1, 100);
                Guard.Against.Null(IssueDate, nameof(IssueDate));
            }

            public override IEnumerable<string> GetEqualityProperties()
            {
                yield return nameof(Description);
                yield return nameof(IssueDate);
            }

            public void Enable() => Enabled = true;

            public void Disable() => Enabled = false;
        }

        public class FooDomainEvent : Event
        {
            public Guid EventId { get; set; }

            public FooDomainEvent(Guid eventId)
            {
                EventId = eventId;
            }
        }
    }
}
