using FluentValidation;

namespace Application.Requests.Breeds.Commands
{
    public class UpdateBreedCommand : BreedCommand
    {
        public Guid ExternalId { get; private set; }

        public UpdateBreedCommand(Guid externalId, string name, int breedTypeId, string family, string origin) : base(name, breedTypeId, family, origin)
        {
            ExternalId = externalId;
        }
    }

    public class UpdateBreedCommandValidator : AbstractValidator<UpdateBreedCommand>
    {
        public UpdateBreedCommandValidator()
        {
            RuleFor(p => p.ExternalId).NotEmpty();
            Include(new BreedCommandValidator());
        }
    }
}
