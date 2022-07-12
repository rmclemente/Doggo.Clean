using Domain.Core.SeedWork;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Requests.Breeds.Commands
{
    public class CreateBreedCommandHandler : IRequestHandler<CreateBreedCommand, CommandResult>
    {
        private readonly IBreedRepository _breedRepository;

        public CreateBreedCommandHandler(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<CommandResult> Handle(CreateBreedCommand request, CancellationToken cancellationToken)
        {
            Breed entity = request;
            _breedRepository.Add(entity);
            await _breedRepository.UnitOfWork.Commit(cancellationToken);
            return CommandResult.CreatedResult().WithResourceId(entity.ExternalId);
        }
    }
}
