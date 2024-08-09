using BankingControlPanel.Application.Features.UserFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

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
        /// <response code="500">We did something wrong.Please try it again.</response>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// 

        [HttpPost]
        [Route("registration")]
        [ProducesResponseType(typeof(RegisterUserCommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registration([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
