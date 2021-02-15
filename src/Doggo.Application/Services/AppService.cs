using Doggo.Infra.CrossCutting.Communication;
using Doggo.Infra.CrossCutting.Messages.Notifications;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doggo.Application.Services
{
    public abstract class AppService
    {
        private readonly IMediatorHandler _mediatorHandler;

        protected AppService(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected async Task SendNotFoundNotification(string field, object value)
        {
            await PublishNotification(field, $"Nenhum registro encontrado para {field} {value}");
        }

        protected async Task SendDomainNotification(IList<ValidationFailure> errors)
        {
            foreach (var error in errors)
                await PublishNotification(error.PropertyName, error.ErrorMessage);
        }

        protected async Task PublishNotification(string errorCode, string errorMessage)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification(errorCode, errorMessage));
        }
    }
}
