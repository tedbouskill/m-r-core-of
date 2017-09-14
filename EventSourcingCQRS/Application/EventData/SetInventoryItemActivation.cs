using DomainCore.Interfaces;

namespace Application.EventData
{
    public class SetInventoryItemActivation : IInventoryItemEventData
    {
        public string Reason { get; set; }
    }
}
