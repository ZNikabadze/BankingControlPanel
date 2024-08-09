using BankingControlPanel.Domain.ClientManagement;

namespace BankingControlPanel.Application.Features.AdminFeatures.DTOs
{
    public record ClientDto(string Email,
            string FirstName,
            string LastName,
            string PersonalId,
            string ProfilePhoto,
            string MobileNumber,
            Sex Sex,
            AddressDto Address,
            IEnumerable<AccountDto> Accounts);
}
