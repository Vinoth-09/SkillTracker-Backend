using Admin.Application.Features.Queries.SearchQuery;
using Admin.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IMediator mediator, ILogger<AdminController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "IsAlive")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<int> IsAlive()
        {
            var response = "Admin API is in good health.";
            _logger.LogInformation(response);
            return Ok(response);
        }
        
        [HttpPost("search",Name ="Search")]
        public async Task<ActionResult<List<Admin.Domain.Models.Profile>>> Search(SearchProfileQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
