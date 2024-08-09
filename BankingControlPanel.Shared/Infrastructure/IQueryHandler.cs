using MediatR;

namespace BankingControlPanel.Shared.Infrastructure
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
               where TQuery : IQuery<TResponse>
               where TResponse : notnull
    {
    }
}
