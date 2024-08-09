using BankingControlPanel.Shared.Infrastructure;

namespace BankingControlPanel.Application.Features.UserFeatures.Commands
{
    internal class RegisterUserCommandHandler() : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        public Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class RegisterUserCommand() : ICommand<RegisterUserCommandResult>;
    public record RegisterUserCommandResult;
}
