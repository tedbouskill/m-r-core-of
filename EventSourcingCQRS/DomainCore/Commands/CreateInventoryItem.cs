using System;

using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore.EventData;
using DomainCore.Interfaces;

namespace DomainCore.Commands
{
    public class CreateInventoryItem : IInventoryItemEvent, ICommand
    {
        public Guid AggregateId { get; }
		public DateTime Timestamp { get; }
		public string EventName { get; }
        public IModelEventData<Guid> EventData { get; }

        public CreateInventoryItem(InventoryItemDto inventoryItem)
        {
            AggregateId = inventoryItem.Id;
            Timestamp = DateTime.UtcNow;
            EventName = "CreateInventoryItem";
            EventData = new CreateInventoryItemData(inventoryItem);
        }
    }
}
