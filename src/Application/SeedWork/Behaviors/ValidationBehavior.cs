using Domain.Core.SeedWork;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Application.SeedWork.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : CommandResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly NotificationContext _notificationContext;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger, IHttpContextAccessor httpContext, NotificationContext notificationContext)
    {
        _validators = validators;
        _logger = logger;
        _contextAccessor = httpContext;
        _notificationContext = notificationContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        SetCulture();

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(request => request.Validate(context))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (!errors.Any())
            return await next();

        AddErrorNotifications(errors);

        return CommandResult.ValidationErrorResult() as TResponse;
    }

    private void AddErrorNotifications(List<ValidationFailure> errors) 
        => errors.ForEach(p => _notificationContext.AddNotification(new Notification(p.PropertyName, p.ErrorMessage)));

    private void SetCulture()
    {
        if (_contextAccessor.HttpContext.Request.Headers.TryGetValue("Culture", out var cultureValues))
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo(cultureValues);
        else
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
    }
}
