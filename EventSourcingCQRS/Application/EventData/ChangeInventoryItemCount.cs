using DomainCore.Interfaces;

namespace DomainCore.EventData
{
    public class ChangeInventoryItemCount : IInventoryItemEventData
    {
        public int Count { get; set; }
    }
}
