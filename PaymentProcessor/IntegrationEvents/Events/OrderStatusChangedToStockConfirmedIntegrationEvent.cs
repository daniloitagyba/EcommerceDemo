using EventBus.Events;

namespace PaymentProcessor.IntegrationEvents.Events
{
    public record OrderStatusChangedToStockConfirmedIntegrationEvent(int OrderId) : IntegrationEvent;

}
