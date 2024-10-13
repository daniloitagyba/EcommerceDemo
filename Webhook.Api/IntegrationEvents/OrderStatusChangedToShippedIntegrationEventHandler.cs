using EventBus.Abstractions;
using Webhook.Api.Model;
using Webhook.Api.Services;

namespace Webhook.Api.IntegrationEvents
{
    public class OrderStatusChangedToShippedIntegrationEventHandler(
        IWebhooksRetriever retriever,
        IWebhooksSender sender,
        ILogger<OrderStatusChangedToShippedIntegrationEventHandler> logger) : IIntegrationEventHandler<OrderStatusChangedToShippedIntegrationEvent>
    {
        public async Task Handle(OrderStatusChangedToShippedIntegrationEvent @event)
        {
            var subscriptions = await retriever.GetSubscriptionsOfType(WebhookType.OrderShipped);

            logger.LogInformation("Received OrderStatusChangedToShippedIntegrationEvent and got {SubscriptionCount} subscriptions to process", subscriptions.Count());

            var whook = new WebhookData(WebhookType.OrderShipped, @event);

            await sender.SendAll(subscriptions, whook);
        }
    }
}
