using BankingControlPanel.Application.Shared.Cache.Abstraction;
using Microsoft.Extensions.Caching.Memory;

namespace BankingControlPanel.Application.Shared.Cache.Concrete
{
    internal class SearchSuggestionCacheService : ISearchSuggestionCacheService
    {
        private readonly IMemoryCache _cache;
        private const string CacheKeyPrefix = "SearchParameters_";

        public SearchSuggestionCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void CacheParameters(string username, string parameters)
        {
            var cacheKey = CacheKeyPrefix + username;
            var cachedParameters = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Adjust as needed
                return new List<string>();
            });

            // Ensure unique parameters and maintain a maximum of 3

            if (!cachedParameters.Contains(parameters))
            {
                cachedParameters.Insert(0, parameters);
                if (cachedParameters.Count > 3)
                {
                    cachedParameters.RemoveAt(3);
                }
                _cache.Set(cacheKey, cachedParameters);
            }
        }

        public IEnumerable<string> GetLastThreeParameters(string username)
        {
            var cacheKey = CacheKeyPrefix + username;
            return _cache.Get<IEnumerable<string>>(cacheKey) ?? new List<string>();
        }
    }
}
