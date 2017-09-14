using DomainCore.Interfaces;

namespace Application.EventData
{
    public class AdjustInventoryItemCount : IInventoryItemEventData
    {
        public uint Delta { get; set; }

        public string Reason { get; set; }
    }
}
