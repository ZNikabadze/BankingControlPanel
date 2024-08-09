using BankingControlPanel.Shared.Infrastructure.Models;

namespace BankingControlPanel.Domain.ClientManagement
{
    public class Account : Entity
    {
        protected Account() { }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
