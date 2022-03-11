using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Profile.API.Filters;
using Profile.Application.Exceptions;
using Profile.Application.Features.Commands.AddProfile;
using Profile.Application.Features.Commands.UpdateProfile;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Profile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [TypeFilter(typeof(GloabalExceptionFillter))]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IMediator mediator, ILogger<ProfileController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "IsAlive")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> IsAlive()
        {
            _logger.LogInformation("Checking Profile API live status.");
            string response = "Profile API is in good health.";
            return Ok(response);
        }

        [HttpPost(Name = "AddProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddProfile([FromBody] AddProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> UpdateProfile([FromBody] UpdateProfileCommand command,
            [FromHeader(Name="x-userid")] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                var errors = new List<ValidationFailure> { new ValidationFailure("", "UserId header missing") };
                throw new ValidationException(errors);
            }

            await _mediator.Send(command);
            return Ok();
        }
    }
}
