using BankingControlPanel.Domain.ClientManagement.ValueObjects;
using BankingControlPanel.Domain.Exceptions;
using BankingControlPanel.Shared.Helpers;
using BankingControlPanel.Shared.Infrastructure.Models;
using System.Text.RegularExpressions;

namespace BankingControlPanel.Domain.ClientManagement
{
    public class Client : AggregateRoot<int>
    {
        protected Client() { }

        protected Client(string email,
            string firstName,
            string lastName,
            string personalId,
            string profilePhoto,
            string mobileNumber,
            Sex sex,
            Address address,
            Account account)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PersonalId = personalId;
            ProfilePhoto = profilePhoto;
            MobileNumber = mobileNumber;
            Sex = sex;
            Address = address;
            Accounts.Add(account);

            ValidateClient();
        }

        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PersonalId { get; private set; }
        public string ProfilePhoto { get; private set; }
        public string MobileNumber { get; private set; }
        public Sex Sex { get; private set; }
        public Address Address { get; private set; }
        public virtual ICollection<Account> Accounts { get; private set; } = new List<Account>();

        public static Client Create(string email,
            string firstName,
            string lastName,
            string personalId,
            string profilePhotoUrl,
            string mobileNumber,
            Sex sex,
            Address address,
            Account account)
        {
            return new Client(email, firstName, lastName, personalId, profilePhotoUrl, mobileNumber, sex, address, account);
        }

        private void ValidateClient()
        {
            if (string.IsNullOrEmpty(Email) || !Regex.IsMatch(Email, Validations.EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)))
                throw new DomainException(DomainErrorCodes.InvalidEmail);

            if (string.IsNullOrEmpty(FirstName) || FirstName.Length >= 60 || !Regex.Matches(FirstName, Validations.NamesRegex, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)).Any(x => x.Success))
                throw new DomainException(DomainErrorCodes.InvalidFirstName);

            if (string.IsNullOrEmpty(LastName) || LastName.Length >= 60 || !Regex.Matches(LastName, Validations.NamesRegex, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)).Any(x => x.Success))
                throw new DomainException(DomainErrorCodes.InvalidLastName);

            if (Sex == Sex.None)
                throw new DomainException(DomainErrorCodes.InvalidGender);

            if (string.IsNullOrEmpty(PersonalId) || PersonalId.Length != 11 || !Regex.Matches(PersonalId, Validations.DigitsRegex, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)).Any(x => x.Success))
                throw new DomainException(DomainErrorCodes.InvalidPersonId);

            if (Accounts.Count() < 1)
                throw new DomainException(DomainErrorCodes.InvalidAccountCount);

            if (!Validations.IsValidMobileNumber(MobileNumber))
                throw new DomainException(DomainErrorCodes.InvalidMobileNumber);
        }
    }
}
