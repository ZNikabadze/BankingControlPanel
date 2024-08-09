namespace BankingControlPanel.Domain.Exceptions
{
    public enum DomainErrorCodes
    {
        None = 0,
        InvalidEmail,
        InvalidFirstName,
        InvalidLastName,
        InvalidGender,
        InvalidPersonId,
        InvalidPhoneNumber,
        InvalidCityName,
        InvalidRole,
        InvalidAccountCount
    }
}
