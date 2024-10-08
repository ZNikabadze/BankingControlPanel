﻿using BankingControlPanel.Application.Features.AdminFeatures.Commands;
using BankingControlPanel.Application.Features.AdminFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.V1
{
    /// <summary>
    /// Client specific controller
    /// </summary>
    /// 

    [Authorize(Roles = "Admin")]
    [Route("api/v1/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Client specific controller
        /// </summary>
        /// 

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Client Creation
        /// </summary>
        /// 
        /// <response code="200">Client has been added</response>
        /// <response code="400">You did something wrong!</response>
        /// <response code="500">We did something wrong.Please try it again.</response>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// 

        [HttpPost]
        [ProducesResponseType(typeof(AddClientCommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddClient([FromBody] AddClientCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Clients query
        /// </summary>
        /// 
        /// <response code="200">Clients are returned</response>
        /// <response code="400">You did something wrong!</response>
        /// <response code="500">We did something wrong.Please try it again.</response>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// 

        [HttpGet]
        [ProducesResponseType(typeof(ClientsQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Clients([FromQuery] ClientsQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        } 
        
        /// <summary>
        /// Search suggestions query
        /// </summary>
        /// 
        /// <response code="200">Suggestions are returned</response>
        /// <response code="400">You did something wrong!</response>
        /// <response code="500">We did something wrong.Please try it again.</response>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// 

        [HttpGet("search-suggestions")]
        [ProducesResponseType(typeof(SearchSuggestionsQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchSuggestions([FromQuery] SearchSuggestionsQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }
    }
}
