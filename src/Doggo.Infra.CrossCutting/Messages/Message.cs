using System;

namespace Doggo.Infra.CrossCutting.Messages
{
    public abstract class Message
    {
        public Guid AggregateId { get; protected set; }
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
