namespace Orders.EventHandler.Events
{
    public class ImageUnussignedFromOrder : IEvent
    {
        public ImageUnussignedFromOrder(string orderId)
        {
            this.OrderId = orderId;
        }

        public string OrderId { get; set; }
    }
}
