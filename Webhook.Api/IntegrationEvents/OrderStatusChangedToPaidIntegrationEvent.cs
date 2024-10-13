using EventBus.Events;

namespace Webhook.Api.IntegrationEvents
{
    public record OrderStatusChangedToPaidIntegrationEvent(int OrderId, IEnumerable<OrderStockItem> OrderStockItems) : IntegrationEvent;
}
