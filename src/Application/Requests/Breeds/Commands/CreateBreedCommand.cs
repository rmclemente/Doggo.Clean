using FluentValidation;

namespace Application.Requests.Breeds.Commands
{
    public class CreateBreedCommand : BreedCommand
    {
        public CreateBreedCommand(string name, int breedTypeId, string family, string origin) : base(name, breedTypeId, family, origin)
        {
        }
    }

    public class CreateBreedCommandValidator : AbstractValidator<CreateBreedCommand>
    {
        public CreateBreedCommandValidator()
        {
            Include(new BreedCommandValidator());
        }
    }
}
