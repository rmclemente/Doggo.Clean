using MediatR;

namespace Domain.Core.SeedWork;

public abstract class Command : Message, IRequest<CommandResult>
{
    public DateTimeOffset Timestamp { get; }

    protected Command()
    {
        Timestamp = DateTime.UtcNow;
    }
}
