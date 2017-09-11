using System;
using DomainCore.Interfaces;

namespace DomainCore
{
    public class InventoryItem : Interfaces.IInventoryItem
    {
		/// <summary>
		/// Unique Id representing a SKU and a location
		/// </summary>
		/// <value>The identifier.</value>
		public Guid Id { get; set; }

		public DateTime LastEventTimestamp { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Count { get; set; }
        public string Note { get; set; }
    }
}
