using BankingControlPanel.Domain.Exceptions;
using BankingControlPanel.Domain.UserManagement.Events;
using BankingControlPanel.Shared.Infrastructure.Models;

namespace BankingControlPanel.Domain.UserManagement
{
    public class User : AggregateRoot<int>
    {
        protected User() { }

        protected User(string userName, UserRole userRole)
        {
            ValidateUser();

            Role = userRole;
            UserName = userName;

            Raise(new UserCreatedEvent());
        }

        public string UserName { get; private set; }
        public UserRole Role { get; private set; }

        public static User CreateUser(string userName, UserRole userRole)
        {
            return new User(userName, userRole);
        }

        private void ValidateUser()
        {
            if (Role == UserRole.None)
                throw new DomainException(DomainErrorCodes.InvalidRole);
        }
    }
}
