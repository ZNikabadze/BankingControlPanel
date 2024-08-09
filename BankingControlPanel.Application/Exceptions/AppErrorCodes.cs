namespace BankingControlPanel.Application.Exceptions
{
    public enum AppErrorCodes: byte
    {
        None = 0,
        UserAlreadyExists,
        UserDoesNotExist,
        InvalidCredentials,
        InvalidGender,
        InvalidFirstName,
        InvalidLastName,
        InvalidPersonalId,
        ClientAlreadyExist
    }
}
