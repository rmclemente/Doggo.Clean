using Domain.Core.SeedWork;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Requests.Breeds.Commands
{
    public class UpdateBreedCommandHandler : IRequestHandler<UpdateBreedCommand, CommandResult>
    {
        private readonly IBreedRepository _breedRepository;

        public UpdateBreedCommandHandler(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<CommandResult> Handle(UpdateBreedCommand request, CancellationToken cancellationToken)
        {
            var entity = await _breedRepository.Get(false, cancellationToken, p => p.ExternalId == request.ExternalId);
            if (entity is null)
                return CommandResult.NotFoundResult();

            Breed source = request;
            entity.CopyDataFrom(source);

            await _breedRepository.UnitOfWork.Commit(cancellationToken);
            return CommandResult.NoContentResult();
        }
    }
}
