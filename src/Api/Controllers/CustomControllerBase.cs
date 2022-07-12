using Domain.Core.SeedWork;
using Microsoft.AspNetCore.Mvc;
using System.Net;
#pragma warning disable CS8603 // Possible null reference return.

namespace Api.Controllers
{
    [ApiController]
    public abstract class CustomControllerBase : ControllerBase
    {
        private readonly NotificationContext _notificationContext;

        protected CustomControllerBase(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        protected string RequestPath => Request.HttpContext.Request.Path.Value;

        /// <summary>
        /// Returns a response with a HTTP status code and a complex Api Response Object.
        /// Implementation of RFC 7807, Problem Details for HTTP APIs, for Bad Requests.
        /// Reference: https://tools.ietf.org/html/rfc7807
        /// </summary>
        /// <param name="response">CommandResponse</param>
        /// <returns>
        ///     BadRequest (400): ValidationProblemDetails;<br />
        ///     Created (201): Location header;<br />
        ///     NoContent (204): No Content;<br />
        ///     NotFound (404): No Content;<br />
        ///     Success (200): Object Response;
        /// </returns>
        protected IActionResult ResponseHandler(CommandResult response)
        {
            if (_notificationContext.HasNotification)
                return BadRequest(GetProblemDetailsResponse());

            return response.StatusCode switch
            {
                HttpStatusCode.Created => Created($"{RequestPath}/{response.ResourceId}", response.Content),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.Forbidden => Forbid(),
                _ => Ok(response),
            };
        }

        protected IActionResult ResponseHandler(QueryResult response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                _ => Ok(response.Content),
            };
        }

        private ValidationProblemDetails GetProblemDetailsResponse()
        {
            var result = _notificationContext.Notifications
                .GroupBy(p => p.Key)
                .ToDictionary(p => p.Key, p => p.Select(v => v.Value).ToArray());

            return new ValidationProblemDetails(result);
        }
    }
}
