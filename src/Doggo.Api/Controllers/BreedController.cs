using Doggo.Application.Dtos;
using Doggo.Application.Interfaces;
using Doggo.Infra.CrossCutting.Messages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Doggo.Api.Controllers
{
    [Route("api/[controller]")]
    public class BreedController : CustomControllerBase
    {
        private readonly IBreedService _breedService;
        //private readonly ILogger<BreedController> _logger;

        public BreedController(IBreedService breedService,
                               INotificationHandler<DomainNotification> domainNotifications) : base(domainNotifications)
        {
            _breedService = breedService;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<BreedDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _breedService.GetAll();
            return Ok(result);
        }

        [HttpGet("{uniqueId:Guid}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid uniqueId)
        {
            var result = await _breedService.Get(uniqueId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] BreedDto dto)
        {
            var result = await _breedService.Add(dto);
            if (IsBadRequest) return BadRequest(GetDomainValidationProblemDetailsContent());
            return Created($"{RequestPath}/{result?.UniqueId}", result);
        }

        [HttpPut("{uniqueId:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromRoute] Guid uniqueId, [FromBody] BreedDto dto)
        {
            var exist = await _breedService.Exist(uniqueId);
            if (!exist) return NotFound();
            var result = await _breedService.Update(uniqueId, dto);
            if (IsBadRequest) return BadRequest(GetDomainValidationProblemDetailsContent());
            return Ok(result);
        }
    }
}
