using Microsoft.Azure.EventGrid.Models;
using System.Threading.Tasks;

namespace Orders.EventHandler.Interfaces
{
    public interface IEventDispatchService
    {
        Task Dispatch(EventGridEvent eventGridEvent);

        SubscriptionValidationResponse DispatchEventGridSubscriptionValidationEvent(EventGridEvent eventGridEvent);
    }
}
