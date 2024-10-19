﻿using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.API.Application.DomainEventHandlers
{
    public partial class OrderCancelledDomainEventHandler
                    : INotificationHandler<OrderCancelledDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly ILogger _logger;
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public OrderCancelledDomainEventHandler(
            IOrderRepository orderRepository,
            ILogger<OrderCancelledDomainEventHandler> logger,
            IBuyerRepository buyerRepository,
            IOrderingIntegrationEventService orderingIntegrationEventService)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _buyerRepository = buyerRepository ?? throw new ArgumentNullException(nameof(buyerRepository));
            _orderingIntegrationEventService = orderingIntegrationEventService;
        }

        public async Task Handle(OrderCancelledDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            OrderingApiTrace.LogOrderStatusUpdated(_logger, domainEvent.Order.Id, OrderStatus.Cancelled);

            var order = await _orderRepository.GetAsync(domainEvent.Order.Id);
            var buyer = await _buyerRepository.FindByIdAsync(order.BuyerId.Value);

            var integrationEvent = new OrderStatusChangedToCancelledIntegrationEvent(order.Id, order.OrderStatus, buyer.Name, buyer.IdentityGuid);
            await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
