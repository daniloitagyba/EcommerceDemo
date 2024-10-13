using Webhook.Api.Model;

namespace Webhook.Api.Services
{
    public interface IWebhooksSender
    {
        Task SendAll(IEnumerable<WebhookSubscription> receivers, WebhookData data);
    }
}
