using EventBus.Events;

namespace PaymentProcessor.IntegrationEvents.Events
{
    public record OrderPaymentFailedIntegrationEvent(int OrderId) : IntegrationEvent;
}
