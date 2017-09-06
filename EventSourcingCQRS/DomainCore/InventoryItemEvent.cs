using System;

namespace DomainCore
{
    public class InventoryItemEvent : Common.EventSourcing.EventModel
    {
		/// <summary>
		/// Unique Id representing a SKU and a location
		/// </summary>
		/// <value>The identifier.</value>
		public Guid AggregateKey { get; set; }

		public DateTime TimeStamp { get; set; }
	}
}
