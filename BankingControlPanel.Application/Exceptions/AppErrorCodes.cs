namespace BankingControlPanel.Application.Exceptions
{
    public enum AppErrorCodes: byte
    {
        None = 0,
        UserAlreadyExists,
        UserDoesNotExist,
        InvalidCredentials,
        InvalidEmail,
        InvalidGender,
        InvalidRole,
        InvalidFirstName,
        InvalidLastName,
        InvalidPersonalId,
        InvalidMobileNumber,
        ClientAlreadyExist
    }
}
