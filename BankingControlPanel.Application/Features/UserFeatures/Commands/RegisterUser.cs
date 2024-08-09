using BankingControlPanel.Application.Exceptions;
using BankingControlPanel.Domain.UserManagement;
using BankingControlPanel.Domain.UserManagement.Repository;
using BankingControlPanel.Shared.Infrastructure;

namespace BankingControlPanel.Application.Features.UserFeatures.Commands
{
    internal class RegisterUserCommandHandler(IUserRepository users, IUnitOfWork _unitOfWork) : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var user = await users.OfName(command.Username, cancellationToken);

            if (user != null)
                throw new AppException(AppErrorCodes.UserAlreadyExists);

            var userToCreate = User.Create(command.Username, BCrypt.Net.BCrypt.HashPassword(command.Password), command.Role);

            await users.Store(userToCreate, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new RegisterUserCommandResult(userToCreate.Id);
        }
    }

    public record RegisterUserCommand(string Username, string Password, UserRole Role) : ICommand<RegisterUserCommandResult>;
    public record RegisterUserCommandResult(int Id);
}
