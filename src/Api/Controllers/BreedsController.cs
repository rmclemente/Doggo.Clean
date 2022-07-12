using Application.Requests.Breeds.Commands;
using Application.Requests.Breeds.Queries;
using Application.Requests.Breeds.Responses;
using Application.SeedWork;
using Domain.Core.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BreedsController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public BreedsController(IMediator mediator, NotificationContext notificationContext) : base(notificationContext)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBreed([FromBody] CreateBreedCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return ResponseHandler(result);
        }

        [HttpGet("{externalId:Guid}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BreedBriefResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActivityByExternalId([FromRoute] Guid externalId, CancellationToken cancellationToken)
        {
            var query = new GetBreedByExternalIdQuery(externalId);
            var response = await _mediator.Send(query, cancellationToken);
            return ResponseHandler(response);
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedResponse<BreedResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllActivities([FromQuery] int page, [FromQuery] int rows, CancellationToken cancellationToken)
        {
            var query = new GetPaginatedBreedQuery(page, rows);
            var response = await _mediator.Send(query, cancellationToken);
            return ResponseHandler(response);
        }
    }
}
