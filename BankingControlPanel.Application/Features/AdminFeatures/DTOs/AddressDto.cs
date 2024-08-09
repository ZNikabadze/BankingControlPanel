using BankingControlPanel.Domain.ClientManagement.ValueObjects;

namespace BankingControlPanel.Application.Features.AdminFeatures.DTOs
{
    public class AddressDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public Address ToDomainModel()
        {
            return new Address(Country, City, Street, ZipCode);
        }
    }
}
