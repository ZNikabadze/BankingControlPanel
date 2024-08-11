using BankingControlPanel.Application.Authentication;
using BankingControlPanel.Application.Shared.Cache.Abstraction;
using BankingControlPanel.Shared.Infrastructure;
using System.Text.Json;

namespace BankingControlPanel.Application.Features.AdminFeatures.Queries
{
    internal class SearchSuggestionsQueryHandler(ApplicationContext _context, ISearchSuggestionCacheService _cacheService) : IQueryHandler<SearchSuggestionsQuery, SearchSuggestionsQueryResult>
    {
        public async Task<SearchSuggestionsQueryResult> Handle(SearchSuggestionsQuery request, CancellationToken cancellationToken)
        {
            return new SearchSuggestionsQueryResult(_cacheService.GetLastThreeParameters(_context.Username).Select(x =>
            {
                return JsonSerializer.Deserialize<ClientsQuery>(x);
            }));
        }
    }

    public record SearchSuggestionsQuery : IQuery<SearchSuggestionsQueryResult>;
    public record SearchSuggestionsQueryResult(IEnumerable<ClientsQuery?> Suggestions);
}
