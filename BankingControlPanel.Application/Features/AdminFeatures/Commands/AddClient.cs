using BankingControlPanel.Application.Exceptions;
using BankingControlPanel.Application.Features.AdminFeatures.DTOs;
using BankingControlPanel.Domain.ClientManagement;
using BankingControlPanel.Domain.ClientManagement.Respository;
using BankingControlPanel.Domain.ClientManagement.Validations;
using BankingControlPanel.Shared.Infrastructure;
using FluentValidation;

namespace BankingControlPanel.Application.Features.AdminFeatures.Commands
{
    internal class AddClientCommandHandler(IClientRepository clients, IUnitOfWork _unitOfWork) : ICommandHandler<AddClientCommand, AddClientCommandResult>
    {
        public async Task<AddClientCommandResult> Handle(AddClientCommand command, CancellationToken cancellationToken)
        {
            var client = await clients.OfEmail(command.Email, cancellationToken);

            if (client != null)
                throw new AppException(AppErrorCodes.ClientAlreadyExist);

            var clientToCreate = Client.Create(command.Email,
                                               command.FirstName,
                                               command.LastName,
                                               command.PersonalId,
                                               command.ProfilePhoto,
                                               command.MobileNumber,
                                               command.Sex,
                                               command.Address.ToDomainModel(),
                                               Account.Create($"{command.FirstName}-{command.LastName}"));

            await clients.Store(clientToCreate, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new AddClientCommandResult(clientToCreate.Id);
        }
    }

    public class AddClientCommand : ICommand<AddClientCommandResult>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public string ProfilePhoto { get; set; }
        public string MobileNumber { get; set; }
        public Sex Sex { get; set; }
        public AddressDto Address { get; set; }
    }

    public record AddClientCommandResult(int Id);
    public class AddClientCommandValidator : AbstractValidator<AddClientCommand>
    {
        public AddClientCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50).Matches(RegexConstants.NamesRegex).WithErrorCode(AppErrorCodes.InvalidFirstName.ToString());
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50).Matches(RegexConstants.NamesRegex).WithErrorCode(AppErrorCodes.InvalidLastName.ToString());
            RuleFor(x => x.Sex).NotEqual(Sex.None).WithErrorCode(AppErrorCodes.InvalidGender.ToString());
            RuleFor(x => x.PersonalId).NotEmpty().Length(11).Matches(RegexConstants.DigitsRegex).WithErrorCode(AppErrorCodes.InvalidPersonalId.ToString());
        }
    }
}
