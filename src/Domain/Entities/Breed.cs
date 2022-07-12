using Ardalis.GuardClauses;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Seedwork;

namespace Domain.Entities
{
    public class Breed : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public BreedType BreedType { get; private set; }
        public string Family { get; private set; }
        public string Origin { get; private set; }

        public Breed(string name, int breedTypeId, string family, string origin)
        {
            Name = name;
            Family = family;
            Origin = origin;
            SetBreedType(breedTypeId);
            Validate();
        }

        protected Breed()
        {
        }

        public override IEnumerable<string> GetEqualityProperties()
        {
            yield return nameof(Name);
            yield return nameof(BreedType);
            yield return nameof(Family);
            yield return nameof(Origin);
        }

        public override void Validate()
        {
            Guard.Against.NullOrWhiteSpace(Name, nameof(Name));
            Guard.Against.OutOfLength(Name, nameof(Name), 3, 150);
            Guard.Against.NullOrWhiteSpace(Family, nameof(Family));
            Guard.Against.OutOfLength(Family, nameof(Family), 3, 150);
            Guard.Against.NullOrWhiteSpace(Origin, nameof(Origin));
            Guard.Against.OutOfLength(Origin, nameof(Origin), 3, 150);
        }

        public void SetBreedType(int breedTypeId)
        {
            if (!BreedType.TryFromId(breedTypeId, out var breedType))
                throw new EnumerationKeyOutOfRangeDomainException(nameof(Breed), nameof(breedTypeId), breedTypeId.ToString(), BreedType.ToJoinedValues());

            BreedType = breedType;
        }
    }
}