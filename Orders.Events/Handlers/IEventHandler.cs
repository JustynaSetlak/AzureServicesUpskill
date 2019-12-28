using Orders.EventHandler.Events;
using System.Threading.Tasks;

namespace Orders.EventHandler.Handlers
{
    public interface IEventHandler<T> where T : class, IEvent
    {
        Task Handle(T eventData);
    }
}
