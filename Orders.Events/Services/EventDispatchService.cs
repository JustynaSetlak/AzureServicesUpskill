using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json.Linq;
using Orders.EventHandler.Events;
using Orders.EventHandler.Handlers;
using Orders.EventHandler.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.EventHandler.Services
{
    public class EventDispatchService : IEventDispatchService
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatchService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch(EventGridEvent eventGridEvent)
        {
            var eventType = Type.GetType($"{eventGridEvent.EventType}, {typeof(IEvent).Assembly.FullName}");
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

            dynamic eventToDispatch = ((JObject)(eventGridEvent.Data)).ToObject(eventType);
            dynamic handler = _serviceProvider.GetService(handlerType);

            await handler.Handle(eventToDispatch);
        }

        public SubscriptionValidationResponse DispatchEventGridSubscriptionValidationEvent(EventGridEvent eventGridEvent)
        {
            var eventData = ((JObject)(eventGridEvent.Data)).ToObject<SubscriptionValidationEventData>();

            var responseData = new SubscriptionValidationResponse()
            {
                ValidationResponse = eventData.ValidationCode
            };

            return responseData;
        }
    }
}
