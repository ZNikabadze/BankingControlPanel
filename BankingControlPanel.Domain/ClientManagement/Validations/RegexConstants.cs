namespace BankingControlPanel.Domain.ClientManagement.Validations
{
    public static class RegexConstants
    {
        public const string NamesRegex = @"^([a-zA-Z]+|[\u10A0-\u10FF]+)$";
        public const string DigitsRegex = @"^[0-9]*$";
        public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
    }
}
