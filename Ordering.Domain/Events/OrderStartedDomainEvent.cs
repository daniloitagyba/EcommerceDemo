using MediatR;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.Domain.Events
{
    public record class OrderStartedDomainEvent(
        Order Order,
        string UserId,
        string UserName,
        int CardTypeId,
        string CardNumber,
        string CardSecurityNumber,
        string CardHolderName,
        DateTime CardExpiration) : INotification;
}
