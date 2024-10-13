using Webhook.Api.Model;

namespace Webhook.Api.Services
{
    public interface IWebhooksRetriever
    {
        Task<IEnumerable<WebhookSubscription>> GetSubscriptionsOfType(WebhookType type);
    }
}
