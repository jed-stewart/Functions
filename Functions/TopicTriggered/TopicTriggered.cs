using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Shared.Interface;
using Shared.Event;
using Data.Domain;
using System.Threading.Tasks;

namespace Functions.TopicTriggered
{
    public class TopicTriggered
    {
        private readonly IOrderService _orderService;
        public TopicTriggered(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        [FunctionName("TopicTriggered")]
        public async Task RunAsync([ServiceBusTrigger("topic", "topic-subscription", Connection = "AzureServiceBus")]
            CreateOrderEvent orderEvent, ILogger logger)
        {

            if (orderEvent is null)
            {
                logger.LogError("Invalid Create Order Event");
                return;
            }

            foreach (var order in orderEvent.orders)
            {
                await _orderService.AddAsync(order);
            }
        }
    }
}