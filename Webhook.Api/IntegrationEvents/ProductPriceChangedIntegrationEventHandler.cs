using EventBus.Abstractions;

namespace Webhook.Api.IntegrationEvents
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        public Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
