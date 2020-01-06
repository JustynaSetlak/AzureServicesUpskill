using Orders.EventHandler.Events;
using System;
using System.Threading.Tasks;

namespace Orders.EventHandler.Handlers
{
    public class OrderPriceModifiedEventHandler : IEventHandler<OrderPriceModified>
    {
        public Task Handle(OrderPriceModified eventData)
        {
            throw new NotImplementedException();
        }
    }
}
