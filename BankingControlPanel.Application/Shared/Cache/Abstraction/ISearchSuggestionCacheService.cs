namespace BankingControlPanel.Application.Shared.Cache.Abstraction
{
    internal interface ISearchSuggestionCacheService
    {
        IEnumerable<string> GetLastThreeParameters(string username);
        void CacheParameters(string username, string parameters);
    }
}
