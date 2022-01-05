using Data.Domain;

namespace Shared.Event
{
    public class CreateOrderEvent
    {
        public List<Order> orders { get; set; } = new List<Order>();
    }
}
