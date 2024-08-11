using BankingControlPanel.Application.Features.UserFeatures.Commands;
using BankingControlPanel.Application.Features.UserFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.V1
{

    /// <summary>
    /// User specific controller
    /// </summary>
    /// 

    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;


        /// <summary>
        /// User specific controller
        /// </summary>
        /// 

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// 
        /// <response code="200">User has been registered</response>
        /// <response code="400">You did something wrong!</response>
        /// <response code="500">We did something wrong. Please try it again.</response>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// 

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegisterUserCommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registration([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Log in
        /// </summary>
        /// 
        /// <response code="200">Token is returned</response>
        /// <response code="400">You did something wrong!</response>
        /// <response code="500">We did something wrong.Please try it again.</response>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// 

        [HttpPost]
        [Route("log-in")]
        [ProducesResponseType(typeof(LogInQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LogIn([FromBody] LogInQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }
    }
}
