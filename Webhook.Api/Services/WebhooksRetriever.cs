using Microsoft.EntityFrameworkCore;
using Webhook.Api.Infrastructure;
using Webhook.Api.Model;

namespace Webhook.Api.Services
{
    public class WebhooksRetriever(WebhooksContext db) : IWebhooksRetriever
    {
        public async Task<IEnumerable<WebhookSubscription>> GetSubscriptionsOfType(WebhookType type)
        {
            return await db.Subscriptions.Where(s => s.Type == type).ToListAsync();
        }
    }
}
