using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doggo.Infra.CrossCutting.Messages.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _domainNotifications;

        public DomainNotificationHandler()
        {
            _domainNotifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _domainNotifications.Add(notification);
            return Task.CompletedTask;
        }

        public List<DomainNotification> GetDomainNotifications() => _domainNotifications;
        public bool HasDomainNotifications() => _domainNotifications.Any();
        public void Dispose() => _domainNotifications = new List<DomainNotification>();
    }
}
