using DomainCore.Interfaces;

namespace Application.EventData
{
    public class SetInventoryItemNameData : IInventoryItemEventData
    {
        public string Name { get; set; }

        public string Reason { get; set; }
    }
}
