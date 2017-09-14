using DomainCore.Interfaces;

namespace Application.EventData
{
    public class SetInventoryItemNoteData : IInventoryItemEventData
    {
        public string Note { get; set; }

        public string Reason { get; set; }
    }
}
