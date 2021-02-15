using Doggo.Infra.CrossCutting.Messages.Notifications;
using System.Threading.Tasks;

namespace Doggo.Infra.CrossCutting.Communication
{
    public interface IMediatorHandler
    {
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
