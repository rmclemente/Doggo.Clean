using MediatR;
using System;

namespace Doggo.Infra.CrossCutting.Messages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime Timestamp { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Timestamp = DateTime.Now;
            Version = 1;
        }
    }
}
