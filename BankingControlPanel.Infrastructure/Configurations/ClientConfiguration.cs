using BankingControlPanel.Domain.ClientManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankingControlPanel.Shared.Extensions;

namespace BankingControlPanel.Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.PersonalId);
            builder.HasIndex(x => x.FirstName);
            builder.HasIndex(x => x.LastName);

            builder.Property(x => x.FirstName).HasMaxLength(59);
            builder.Property(x => x.LastName).HasMaxLength(59);
            builder.Property(x => x.PersonalId).IsRequired().HasMaxLength(11);
            builder.Property(x => x.Address).HasJsonConversion();

            builder.HasMany(x => x.Accounts).WithOne(x => x.Client);
        }
    }
}
