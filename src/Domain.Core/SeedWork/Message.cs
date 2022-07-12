namespace Domain.Core.SeedWork;

public abstract class Message
{
    public Guid MessageId { get; }
    public string MessageType { get; }

    protected Message()
    {
        MessageId = Guid.NewGuid();
        MessageType = GetType().Name;
    }
}
