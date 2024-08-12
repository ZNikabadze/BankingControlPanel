using BankingControlPanel.Domain.Exceptions;
using BankingControlPanel.Domain.UserManagement.Events;
using BankingControlPanel.Shared.Infrastructure.Models;

namespace BankingControlPanel.Domain.UserManagement
{
    public class User : AggregateRoot<int>
    {
        protected User() { }

        protected User(string username, string passwordHash, UserRole userRole)
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = userRole;

            ValidateUser();

            Raise(new UserCreatedEvent()); // We can use event to create read models or handle for business process purposes
        }

        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }

        public static User Create(string username, string passwordHash, UserRole userRole)
        {
            return new User(username, passwordHash, userRole);
        }

        private void ValidateUser()
        {
            if (Role == UserRole.None)
                throw new DomainException(DomainErrorCodes.InvalidRole);
        }
    }
}
