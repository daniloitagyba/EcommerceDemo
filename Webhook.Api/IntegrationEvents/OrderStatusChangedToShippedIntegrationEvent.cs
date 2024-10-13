using EventBus.Events;

namespace Webhook.Api.IntegrationEvents
{
    public record OrderStatusChangedToShippedIntegrationEvent(int OrderId, string OrderStatus, string BuyerName) : IntegrationEvent;

}
