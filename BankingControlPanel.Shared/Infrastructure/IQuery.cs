using MediatR;

namespace BankingControlPanel.Shared.Infrastructure
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
                where TResponse : notnull
    {
    }
}
