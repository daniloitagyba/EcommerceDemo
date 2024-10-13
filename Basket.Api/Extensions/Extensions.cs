using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Basket.Api.IntegrationEvents.EventHandling;
using Basket.Api.IntegrationEvents.Events;
using Basket.Api.Repositories;
using EventBus.Extensions;
using EventBusRabbitMQ;
using ServiceDefaults.Extensions;

namespace Basket.Api.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddDefaultAuthentication();

        builder.AddRedisClient("redis");

        builder.Services.AddSingleton<IBasketRepository, RedisBasketRepository>();

        builder.AddRabbitMqEventBus("eventbus")
            .AddSubscription<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>();
    }
}