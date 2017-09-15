using System;

using DomainCore.Interfaces;

namespace DomainCore
{
    /// <summary>
    /// Represents an inventory item's current state
    /// </summary>
    public class InventoryItemDto : Interfaces.IInventoryItem
    {
        public InventoryItemDto() { }
        
        public InventoryItemDto(InventoryItemAggregate inventoryItemAggregate)
        {
            Id = inventoryItemAggregate.AggregateId;
            LastEventTimestamp = inventoryItemAggregate.LastEventTimestamp;
            Name = inventoryItemAggregate.Name;
			IsActive = inventoryItemAggregate.IsActive;
			Count = inventoryItemAggregate.Count;
			Note = inventoryItemAggregate.Note;
		}

		public Guid Id { get; set; }

		public DateTime LastEventTimestamp { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Count { get; set; }
        public string Note { get; set; }
    }
}
