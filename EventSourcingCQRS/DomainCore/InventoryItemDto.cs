using System;
using DomainCore.Interfaces;

namespace DomainCore
{
    /// <summary>
    /// Represents an inventory item's current state
    /// </summary>
    public class InventoryItemDto : Interfaces.IInventoryItem
    {
		public Guid Id { get; set; }

		public DateTime LastEventTimestamp { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; } = false;
        public int Count { get; set; } = 0;
        public string Note { get; set; }
    }
}
