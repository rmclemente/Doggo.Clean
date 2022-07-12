using Domain.Core.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.SeedWork.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            _logger.LogInformation("Handling incoming request: {@Request}", request);
            var response = await next();

            if ((int)response.StatusCode >= 400)
                _logger.LogWarning("Handled request result: {@Response}", response);
            else
                _logger.LogInformation("Handled request result: {@Response}", response);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{ExceptionType}: {Message}. Request: {@Request}", ex.GetType().Name, ex.Message, request);
            throw;
        }
    }
}
