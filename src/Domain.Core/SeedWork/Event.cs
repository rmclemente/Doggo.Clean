using MediatR;

namespace Domain.Core.SeedWork;

public abstract class Event : Message, INotification
{
    public DateTimeOffset Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.UtcNow;
    }
}
