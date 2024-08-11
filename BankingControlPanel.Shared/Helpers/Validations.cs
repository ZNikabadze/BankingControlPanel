using PhoneNumbers;

namespace BankingControlPanel.Shared.Helpers
{
    public static class Validations
    {
        public const string NamesRegex = @"^([a-zA-Z]+|[\u10A0-\u10FF]+)$";
        public const string DigitsRegex = @"^[0-9]*$";
        public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        public static bool IsValidMobileNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var number = phoneNumberUtil.Parse(phoneNumber, "GE");
                return phoneNumberUtil.IsValidNumber(number);
            }
            catch (NumberParseException e)
            {
                return false;
            }
        }
    }
}
