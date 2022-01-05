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
        private readonly IVisitService _visitService;
        public TopicTriggered(
            IVisitService visitService)
        {
            _visitService = visitService;
        }

        [FunctionName("TopicTriggered")]
        public async Task RunAsync([ServiceBusTrigger("topic", "topic-subscription", Connection = "AzureServiceBus")]
            CreateVisitEvent visitEvent, ILogger logger)
        {

            if (visitEvent is null)
            {
                logger.LogError("Invalid Create Visit Event");
                return;
            }

            foreach (var visit in visitEvent.Visits)
            {
                await _visitService.AddAsync(visit);
            }
        }
    }
}