using BankingControlPanel.Application.Exceptions;
using BankingControlPanel.Domain.UserManagement;
using BankingControlPanel.Domain.UserManagement.Repository;
using BankingControlPanel.Shared.Infrastructure;

namespace BankingControlPanel.Application.Features.UserFeatures.Commands
{
    internal class RegisterUserCommandHandler(IUserRepository users) : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var user = await users.OfName(command.UserName, cancellationToken);

            if (user != null)
                throw new AppException(AppErrorCodes.UserAlreadyExists);

            var gela = User.CreateUser(command.UserName, command.Role);

            await users.Store(gela, cancellationToken);

            return new RegisterUserCommandResult(gela.Id);
        }
    }

    public record RegisterUserCommand(string UserName, UserRole Role) : ICommand<RegisterUserCommandResult>;
    public record RegisterUserCommandResult(int id);
}
