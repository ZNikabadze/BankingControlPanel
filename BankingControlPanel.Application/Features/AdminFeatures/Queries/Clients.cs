using BankingControlPanel.Application.Authentication;
using BankingControlPanel.Application.Features.AdminFeatures.DTOs;
using BankingControlPanel.Application.Shared.Cache.Abstraction;
using BankingControlPanel.Domain.ClientManagement;
using BankingControlPanel.Infrastructure.DataAccess;
using BankingControlPanel.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace BankingControlPanel.Application.Features.AdminFeatures.Queries
{
    internal class ClientsQueryHandler(BankingControlPanelDbContext _db, ISearchSuggestionCacheService _cacheService, ApplicationContext _context) : IQueryHandler<ClientsQuery, ClientsQueryResult>
    {
        public async Task<ClientsQueryResult> Handle(ClientsQuery query, CancellationToken cancellationToken)
        {
            var clients = _db.Set<Client>().AsQueryable(); // For read side we can create read model,with denormalized fields for high read throughput

            if (!string.IsNullOrEmpty(query.Address))
            {
                var searchTermWithWildCard = "%" + query.Address + "%";

                clients = _db.Set<Client>().FromSqlInterpolated($"select * from \"dbo\".\"Clients\" where \"Address\" like {searchTermWithWildCard}");
            }

            if (!string.IsNullOrEmpty(query.Email))
                clients = clients.Where(x => x.Email.Contains(query.Email));

            if (!string.IsNullOrEmpty(query.FirstName))
                clients = clients.Where(x => x.FirstName.Contains(query.FirstName));

            if (!string.IsNullOrEmpty(query.LastName))
                clients = clients.Where(x => x.LastName.Contains(query.LastName));

            if (!string.IsNullOrEmpty(query.MobileNumber))
                clients = clients.Where(x => x.MobileNumber.Contains(query.MobileNumber));

            if (query.Sex is not null)
                clients = clients.Where(x => x.Sex == query.Sex);

            if (!string.IsNullOrEmpty(query.PersonalId))
                clients = clients.Where(x => x.PersonalId.Contains(query.PersonalId));

            if (!string.IsNullOrEmpty(query.PersonalId))
                clients = clients.Where(x => x.PersonalId.Contains(query.PersonalId));

            var peopleToReturn =   clients.Skip((query.PageNumber ?? 0) * (query.PageSize ?? 10)) // For high performance here we could use cursor pagination
                                          .Take(query.PageSize ?? 10)
                                          .AsNoTracking()
                                          .OrderBy(query.OrderBy ?? "CreatedAt", "asc")
                                          .AsQueryable();

            _cacheService.CacheParameters(_context.Username, JsonSerializer.Serialize(query));

            return new ClientsQueryResult(peopleToReturn.Select(x => new ClientDto(x.Email,
                                                                x.FirstName,
                                                                x.LastName,
                                                                x.PersonalId,
                                                                x.ProfilePhoto,
                                                                x.MobileNumber,
                                                                x.Sex,
                                                                new AddressDto
                                                                {
                                                                    Country = x.Address.Country,
                                                                    City = x.Address.City,
                                                                    Street = x.Address.Street,
                                                                    ZipCode = x.Address.ZipCode,
                                                                },
                                                                x.Accounts.Select(e => new AccountDto(e.Username)))));
        }
    }
    public record ClientsQuery(string? Email,
        string? FirstName,
        string? LastName,
        string? MobileNumber,
        Sex? Sex,
        string? Address,
        string? PersonalId,
        int? PageNumber,
        int? PageSize,
        string? OrderBy) : IQuery<ClientsQueryResult>;
    public record ClientsQueryResult(IEnumerable<ClientDto> Clients);
}
