using BankingControlPanel.Domain.Exceptions;
using BankingControlPanel.Domain.UserManagement.Events;
using BankingControlPanel.Shared.Infrastructure.Models;

namespace BankingControlPanel.Domain.UserManagement
{
    public class User : AggregateRoot<int>
    {
        protected User(UserRole userRole)
        {
            ValidateUser();

            Role = userRole;

            Raise(new UserCreatedEvent());
        }

        public UserRole Role { get; private set; }

        public static User CreateUser(UserRole userRole)
        {
            return new User(userRole);
        }

        private void ValidateUser()
        {
            if (Role == UserRole.None)
                throw new DomainException(DomainErrorCodes.InvalidRole);
        }
    }
}
