using Domain.Core.SeedWork;
using Domain.Entities;
using FluentValidation;

namespace Application.Requests.Breeds.Commands
{
    public class BreedCommand : Command
    {
        public string Name { get; private set; }
        public int BreedTypeId { get; private set; }
        public string Family { get; private set; }
        public string Origin { get; private set; }

        public BreedCommand(string name, int breedTypeId, string family, string origin)
        {
            Name = name;
            BreedTypeId = breedTypeId;
            Family = family;
            Origin = origin;
        }

        public static implicit operator Breed(BreedCommand command)
        {
            return new Breed(command.Name, command.BreedTypeId, command.Family, command.Origin);
        }
    }

    public class BreedCommandValidator : AbstractValidator<BreedCommand>
    {
        public BreedCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(150);
            RuleFor(p => p.Family).NotEmpty().MaximumLength(150);
            RuleFor(p => p.Origin).NotEmpty().MaximumLength(150);
            RuleFor(p => p.BreedTypeId).NotEmpty();
            RuleFor(p => p.BreedTypeId).Must(p => BreedType.TryFromId(p, out _));
        }
    }
}
