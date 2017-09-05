using System;
namespace DomainCore
{
    public class InventoryItem
    {
    	/// <summary>
    	/// Unique Id representing a SKU and a location
    	/// </summary>
    	/// <value>The identifier.</value>
    	public Guid Id { get; set; }

        public DateTime LastEventTimestamp { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

    	public int Count { get; set; }

        public string Note { get; set; }
    }
}
