using Microsoft.Azure.EventGrid;

namespace Orders.EventHandler.Interfaces
{
    public interface IEventGridClientProvider
    {
        EventGridClient Create(string key);
    }
}
