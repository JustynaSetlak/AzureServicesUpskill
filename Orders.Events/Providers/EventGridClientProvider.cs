using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Orders.EventHandler.Interfaces;

namespace Orders.EventHandler.Providers
{
    public class EventGridClientProvider : IEventGridClientProvider
    {
        public EventGridClient Create(string key)
        {
            var topicCredentials = new TopicCredentials(key);
            var eventGridClient = new EventGridClient(topicCredentials);

            return eventGridClient;
        }
    }
}
