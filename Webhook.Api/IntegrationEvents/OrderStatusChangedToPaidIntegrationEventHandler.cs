using EventBus.Abstractions;
using Webhook.Api.Model;
using Webhook.Api.Services;

namespace Webhook.Api.IntegrationEvents
{
    public class OrderStatusChangedToPaidIntegrationEventHandler(
        IWebhooksRetriever retriever,
        IWebhooksSender sender,
        ILogger<OrderStatusChangedToShippedIntegrationEventHandler> logger) : IIntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
    {
        public async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
        {
            var subscriptions = await retriever.GetSubscriptionsOfType(WebhookType.OrderPaid);

            logger.LogInformation("Received OrderStatusChangedToShippedIntegrationEvent and got {SubscriptionsCount} subscriptions to process", subscriptions.Count());

            var whook = new WebhookData(WebhookType.OrderPaid, @event);

            await sender.SendAll(subscriptions, whook);
        }
    }
}
