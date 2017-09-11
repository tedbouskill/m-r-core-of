using System;

namespace DomainCore
{
    /// <summary>
    /// Used to represent an Inventory Item in data stores
    /// </summary>
    public class InventoryItemEventDto
    {
		public Guid AggregateKey { get; set; }

		public DateTime Timestamp { get; set; }

		public string EventName { get; set; }

        public string EventObjJson { get; set; }
    }
}
