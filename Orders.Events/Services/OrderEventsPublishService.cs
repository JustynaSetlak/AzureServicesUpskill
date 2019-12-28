using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Options;
using Orders.Common.Config;
using Orders.EventHandler.Events;
using Orders.EventHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.EventHandler.Services
{
    public class OrderEventsPublishService : IOrderEventsPublishService
    {
        private readonly IEventGridClient _eventGridClient;
        private readonly string _topicHostname;

        public OrderEventsPublishService(IOptions<EventGridConfig> eventGridConfigOptions, IEventGridClientProvider eventGridClientProvider)
        {
            _eventGridClient = eventGridClientProvider.Create(eventGridConfigOptions.Value.Key);
            _topicHostname = new Uri(eventGridConfigOptions.Value.Endpoint).Host;
        }

        public async Task PublishEvent(IEvent eventToPublish)
        {
            var eventType = eventToPublish.GetType().FullName;

            var sampleEvent = new EventGridEvent()
            {
                Id = Guid.NewGuid().ToString(),
                EventType = eventType,
                Data = eventToPublish,
                EventTime = DateTime.UtcNow,
                Subject = eventType,
                DataVersion = "1.0"
            };

            await _eventGridClient.PublishEventsAsync(_topicHostname, new List<EventGridEvent> { sampleEvent });
        }
    }
}
