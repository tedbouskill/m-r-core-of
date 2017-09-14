using DomainCore.Interfaces;

namespace Application.EventData
{
    public class SetInventoryItemCountData : IInventoryItemEventData
    {
        public int Count { get; set; }

        public string Reason { get; set; }
    }
}
