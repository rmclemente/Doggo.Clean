using MediatR;

namespace Domain.Core.SeedWork;

public class Notification : Message, INotification
{
    public string Key { get; private set; }
    public string Value { get; private set; }
    public DateTime Timestamp { get; private set; }

    public Notification(string key, string value)
    {
        Key = key;
        Value = value;
        Timestamp = DateTime.Now;
    }
}
