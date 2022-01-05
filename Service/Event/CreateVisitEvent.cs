using Data.Domain;

namespace Shared.Event
{
    public class CreateVisitEvent
    {
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
