

namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger,
                                                IPublishEndpoint publisher,
                                                IFeatureManager featureManager) :
        INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                // create the integration event
                var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
                // publish it 
                await publisher.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
        }
    }
}
