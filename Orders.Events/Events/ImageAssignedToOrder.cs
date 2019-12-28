namespace Orders.EventHandler.Events
{
    public class ImageAssignedToOrder : IEvent
    {
        public ImageAssignedToOrder(string orderId, string imageUrl)
        {
            this.OrderId = orderId;
            this.ImageUrl = imageUrl;
        }

        public string OrderId { get; set; }

        public string ImageUrl { get; set; }
    }
}
