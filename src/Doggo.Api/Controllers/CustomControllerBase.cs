using Doggo.Infra.CrossCutting.Messages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Doggo.Api.Controllers
{
    [ApiController]
    public abstract class CustomControllerBase : ControllerBase
    {
        private readonly DomainNotificationHandler _domainNotifications;

        protected CustomControllerBase(INotificationHandler<DomainNotification> domainNotifications)
        {
            _domainNotifications = domainNotifications as DomainNotificationHandler;
        }

        protected bool IsBadRequest => _domainNotifications.HasDomainNotifications();

        protected IReadOnlyCollection<DomainNotification> DomainNotifications => _domainNotifications.GetDomainNotifications();

        protected string RequestPath => Request.HttpContext.Request.Path.Value;

        /// <summary>
        /// Returns a response with a HTTP status code and a complex Api Response Object.
        /// Implementation of RFC 7807, Problem Details for HTTP APIs, for Bad Requests.
        /// Reference: https://tools.ietf.org/html/rfc7807
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        ///     Success: Ok(ApiSuccessResponse successResponse)<br />
        ///     Problem: BadRequest(ValidationProblemDetails(IDictionary<string, string[]> errors))
        /// </returns>
        protected IActionResult ApiResponse(object data, int statusCode = StatusCodes.Status200OK)
        {
            if (IsBadRequest)
                return BadRequest(GetDomainValidationProblemDetailsContent());

            return StatusCode(statusCode, data);
        }

        protected IActionResult ApiResponse(int statusCode)
        {
            return ApiResponse(null, statusCode);
        }
        
        protected IActionResult ApiResponse()
        {
            return ApiResponse(null, StatusCodes.Status204NoContent);
        }

        protected ValidationProblemDetails GetDomainValidationProblemDetailsContent()
        {
            var result = new Dictionary<string, string[]>();

            foreach (var notification in DomainNotifications)
            {
                if (!result.ContainsKey(notification.Key))
                    result.Add(notification.Key, DomainNotifications.Where(p => p.Key == notification.Key)
                        .Select(p => p.Value)
                        .ToArray());
            }

            return new ValidationProblemDetails(result);
        }

        protected ValidationProblemDetails GetCustomValidationProblemDetailsContent(string field, string message)
        {
            var resultDictionary = new Dictionary<string, string[]>
            {
                {
                    field, 
                    new [] { message }
                }
            };

            return new ValidationProblemDetails(resultDictionary);
        }

        /// <summary>
        /// Add the Location header where the resource can be found.
        /// </summary>
        /// <param name="resourceKey">Id or keyword that identifies the resource</param>
        protected void AddResourceLocationHeader(object resourceKey)
        {
            if (!IsBadRequest)
                Response.Headers.Add("Location", $"{Request.HttpContext.Request.Path.Value}/{resourceKey}");
        }
    }
}
