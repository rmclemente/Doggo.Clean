using Doggo.Infra.CrossCutting.Messages.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace Doggo.Infra.CrossCutting.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}
