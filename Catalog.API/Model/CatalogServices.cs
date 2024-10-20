using Catalog.API;
using Catalog.API.Infrastructure;
using Catalog.API.IntegrationEvents;
using Catalog.API.Services;


public class CatalogServices(
    CatalogContext context,
    ICatalogAI catalogAI,
    IOptions<CatalogOptions> options,
    ILogger<CatalogServices> logger,
    ICatalogIntegrationEventService eventService)
{
    public CatalogContext Context { get; } = context;
    public ICatalogAI CatalogAI { get; } = catalogAI;
    public IOptions<CatalogOptions> Options { get; } = options;
    public ILogger<CatalogServices> Logger { get; } = logger;
    public ICatalogIntegrationEventService EventService { get; } = eventService;
};
