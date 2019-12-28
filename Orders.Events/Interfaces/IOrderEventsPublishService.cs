using Orders.EventHandler.Events;
using System.Threading.Tasks;

namespace Orders.EventHandler.Interfaces
{
    public interface IOrderEventsPublishService
    {
        Task PublishEvent(IEvent eventToPublish);
    }
}
