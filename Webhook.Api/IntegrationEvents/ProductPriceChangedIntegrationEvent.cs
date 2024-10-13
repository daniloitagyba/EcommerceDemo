using EventBus.Events;

namespace Webhook.Api.IntegrationEvents
{
    public record ProductPriceChangedIntegrationEvent(int ProductId, decimal NewPrice, decimal OldPrice) : IntegrationEvent;
}
