using EventBus.Events;

namespace Basket.Api.IntegrationEvents.Events;

[Serializable]
public record OrderStartedIntegrationEvent(string UserId) : IntegrationEvent;