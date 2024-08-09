using BankingControlPanel.Shared.Infrastructure.Models;

namespace BankingControlPanel.Domain.ClientManagement
{
    public class Account : Entity
    {
        protected Account() { }

        protected Account(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }

        public static Account Create(string username)
        {
            return new Account(username);
        }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
