using Catalog.API;
using Catalog.API.Infrastructure;
using Catalog.API.IntegrationEvents;
using Catalog.API.IntegrationEvents.EventHandling;
using Catalog.API.IntegrationEvents.Events;
using Catalog.API.Services;
using EventBus.Extensions;
using EventBusRabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CatalogContext>("catalogdb", configureDbContextOptions: dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(builder =>
            {
                builder.UseVector();
            });
        });

        // REVIEW: This is done for development ease but shouldn't be here in production
        builder.Services.AddMigration<CatalogContext, CatalogContextSeed>();

        // Add the integration services that consume the DbContext
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<CatalogContext>>();

        builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

        builder.AddRabbitMqEventBus("eventbus")
               .AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>()
               .AddSubscription<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();

        builder.Services.AddOptions<CatalogOptions>()
            .BindConfiguration(nameof(CatalogOptions));

        if (builder.Configuration["AI:Onnx:EmbeddingModelPath"] is string modelPath &&
            builder.Configuration["AI:Onnx:EmbeddingVocabPath"] is string vocabPath)
        {
#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.Services.AddBertOnnxTextEmbeddingGeneration(modelPath, vocabPath);
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        }
        else if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("openai")))
        {
            builder.AddAzureOpenAIClient("openai");
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.Services.AddOpenAITextEmbeddingGeneration(builder.Configuration["AIOptions:OpenAI:EmbeddingName"] ?? "text-embedding-3-small");
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        }

        builder.Services.AddSingleton<ICatalogAI, CatalogAI>();
    }
}
