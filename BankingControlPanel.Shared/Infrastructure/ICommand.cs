using MediatR;

namespace BankingControlPanel.Shared.Infrastructure
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
